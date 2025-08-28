using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Helpers;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;
using ProjectManager.Application.Models.Requests.Task;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.Interfaces.Repositories;
using ProjectManager.Domain.Repositories;
using ProjectManager.Utils;
using System.Security.Claims;

namespace ProjectManager.Application.Services;

public class ProjectService(
    IProjectRepository projectRepository, 
    IUserRepository userRepository,
    ITaskRepository taskRepository,
    ILogger<IProjectService> logger
    ) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly ILogger<IProjectService> _logger = logger;

    public async Task<GenericResponse<ProjectDto>> Create(Claim claim, CreateProjectRequest request)
    {
        try
        {
            var executor = GetExecutor(claim);

            if (_projectRepository.IfExistsProjectStatus(request.StatusId) == false)
            {
                throw new NotFoundException(ResponseConsts.ProjectStatusNotFound);
            }

            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                StatusId = request.StatusId,
                Banner = request.Banner,
                CreatedBy = executor.UserId,
                UpdatedBy = executor.UserId,
                ProjectsUsers = [
                    new ProjectsUser
                    {
                        UserId = executor.UserId
                    }
                ]
            };
            
            await _projectRepository.Create(project);
            _logger.LogInformation("Proyecto creado: {ProjectId}", project.ProjectId);

            return ResponseHelper.Create(Map(project), message: ResponseConsts.ProjectCreated, statusCode: ResponseHttpCodes.Created);
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    public async Task<GenericResponse<ProjectDto>> Update(Guid projectId, Claim claim, UpdateProjectRequest request)
    {
        try
        {
            var executor = GetExecutor(claim);

            if (request.StatusId.HasValue && _projectRepository.IfExistsProjectStatus(request.StatusId.Value) == false)
            {
                throw new NotFoundException(ResponseConsts.ProjectStatusNotFound);
            }

            var getProject = GetProject(projectId);

            getProject.Name = request.Name ?? getProject.Name;
            getProject.Description = request.Description ?? getProject.Description;
            getProject.StatusId = request.StatusId ?? getProject.StatusId;
            getProject.Banner = request.Banner ?? getProject.Banner;

            getProject.UpdatedBy = executor.UserId;
            getProject.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.Update(getProject);

            return ResponseHelper.Create(Map(getProject), message: ResponseConsts.ProjectUpdated, statusCode: ResponseHttpCodes.Updated);
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    public async Task<GenericResponse<bool>> Remove(Claim claim, Guid projectId)
    {
        try
        {
            var getProject = GetProject(projectId);

            await _projectRepository.Delete(getProject);

            return ResponseHelper.Create(true, message: ResponseConsts.ProjectDeleted, statusCode: ResponseHttpCodes.Success);
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    public GenericResponse<ProjectDto> Get(Guid projectId)
    {
        try
        {
            var project = _projectRepository.Get(projectId)
                ?? throw new NotFoundException(ResponseConsts.ProjectNotFound);

            return ResponseHelper.Create(Map(project));
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    public GenericResponse<List<ProjectDto>> Get(GetProjectRequest request)
    {
        try
        {
            var queryable = _projectRepository
                .Queryable();
  

            if (string.IsNullOrWhiteSpace(request.Name) == false)
            {
                queryable = queryable.Where(x => x.Name.Contains(request.Name));
            }

            if (string.IsNullOrWhiteSpace(request.Description) == false)
            {
                queryable = queryable.Where(x => x.Description.Contains(request.Description));
            }

            if (request.StatusId.HasValue)
            {
                queryable = queryable.Where(x => x.StatusId == request.StatusId.Value);
            }

            List<ProjectDto> mapped = [];

            foreach (var project in queryable.ToList())
            {
                mapped.Add(Map(project));
            }

            return ResponseHelper.Create(mapped);
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    private static ProjectDto Map(Project project)
    {
        try
        {
            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Banner = project.Banner,
                StatusId = project.StatusId,
                CreatedBy = project.CreatedBy,
                UpdatedBy = project.UpdatedBy,
                UpdatedAt = project.UpdatedAt,
                CreatedAt = project.CreatedAt,
                Tasks = project.Tasks.Select(task =>
                {
                    return new TaskDto
                    {
                        TaskId = task.TaskId,
                        ProjectId = task.ProjectId,
                        UserId = task.UserId,
                        Name = task.Name,
                        Description = task.Description,
                        StatusId = task.StatusId,
                        CreatedAt = task.CreatedAt,
                        CreatedBy = task.CreatedBy,
                        UpdatedAt = task.CreatedAt,
                        UpdatedBy = task.UpdatedBy,
                    };
                }).ToList(),
                Members = project.ProjectsUsers.Select(x =>
                {
                    var user = x.User;

                    return new UserDto {
                        UserId = user.UserId,
                        EmailAddress = user.EmailAddress,
                        Identification = user.Identification,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        IsActive = user.IsActive,
                        RoleId = user.RoleId,
                        CreatedAt = user.CreatedAt,
                        CreatedBy = user.CreatedBy,
                        UpdatedAt = user.UpdatedAt,
                        UpdatedBy = user.UpdatedBy
                    };
                }).ToList()
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GenericResponse<ProjectDto>> AddMember(Claim claim, Guid projectId, Guid userId)
    {
        try
        {
            var executor = GetExecutor(claim);
            var project = GetProject(projectId);

            var user = _userRepository.Get(userId)
                ?? throw new NotFoundException(ResponseConsts.UserNotFound);

            if (_projectRepository.IfIsMemberForProject(project.ProjectId, user.UserId))
            {
                throw new BadRequestException("El usuario ya forma parte del proyecto");
            }

            var projectUser = new ProjectsUser
            {
                UserId = userId
            };

            project.ProjectsUsers.Add(projectUser);
            await _projectRepository.Update(project);

            return ResponseHelper.Create(Map(project), message: "El usuario ahora forma parte del grupo");
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    public async Task<GenericResponse<ProjectDto>> RemoveMember(Claim claim, Guid projectId, Guid userId)
    {
        try
        {
            var executor = GetExecutor(claim);
            var project = GetProject(projectId);

            var user = _userRepository.Get(userId)
                ?? throw new NotFoundException(ResponseConsts.UserNotFound);

            if (!_projectRepository.IfIsMemberForProject(project.ProjectId, user.UserId))
            {
                throw new BadRequestException("El usuario no forma parte del proyecto");
            }

            var projectUser = project.ProjectsUsers.FirstOrDefault(x => x.UserId == user.UserId)
                ?? throw new NotFoundException("El usuario del proyecto no pudo encontrarse");


            project.ProjectsUsers.Remove(projectUser);
            await _projectRepository.Update(project);

            return ResponseHelper.Create(Map(project), message: "El usuario ya no forma parte del grupo");
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    private User GetExecutor(Claim claim)
    {
        try
        {
            var executorId = Parser.ToGuid(claim.Value) ?? throw new ArgumentNotFoundException(ResponseConsts.UserIdentityNotFound);
            var executor = _userRepository.Get(executorId) ?? throw new ArgumentException(ResponseConsts.UserIdentityNotFound);

            return executor;
        }
        catch (Exception)
        {

            throw;
        }
    }

    private Project GetProject(Guid projectId)
    {
        try
        {
            var project = _projectRepository.Get(projectId)
                ?? throw new NotFoundException(ResponseConsts.ProjectNotFound);

            return project;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<GenericResponse<ProjectDto>> CreateTask(Guid projectId, Claim claim, CreateTaskRequest request)
    {
        try
        {
            if (_taskRepository.IfExistsTaskStatus(request.StatusId) == false)
            {
                throw new NotFoundException(ResponseConsts.TaskNotFound);
            }

            var executor = GetExecutor(claim);
            var project = GetProject(projectId);

            var createTask = new Domain.Context.Task
            {
                Name = request.Name,
                Description = request.Description,
                StatusId = request.StatusId,
                ProjectId = projectId,
                CreatedBy = executor.UserId,
                UpdatedBy = executor.UserId,
            };

            await _taskRepository.Create(createTask);

            return ResponseHelper.Create(Map(project), message: ResponseConsts.TaskCreated(project.Name), statusCode: ResponseHttpCodes.Created);
        }
        catch (Exception e)
        {
            _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    public GenericResponse<ProjectDto> ChangeTaskStatus(Guid projectId, int taskId, Claim claim, int statusId)
    {
        throw new NotImplementedException();
    }

    public GenericResponse<ProjectDto> RemoveTask(Guid projectId, int taskId, Claim claim)
    {
        throw new NotImplementedException();
    }

    public GenericResponse<ProjectDto> AssignTaskToMember(Guid projectId, int taskId, Claim claim, Guid userAssignId)
    {
        throw new NotImplementedException();
    }

    public GenericResponse<ProjectDto> UnassignTaskToMember(Guid projectId, int taskId, Claim claim, Guid userAssignId)
    {
        throw new NotImplementedException();
    }
}