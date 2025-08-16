using ProjectManager.Application.Dtos;
using ProjectManager.Application.Helpers;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.Interfaces.Repositories;
using ProjectManager.Utils;

namespace ProjectManager.Application.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    
    public async Task<GenericResponse<ProjectDto>> Create(CreateProjectRequest request)
    {
        try
        {
            // Validar que el estado del proyecto exista
            if (_projectRepository.IfExistsProjectStatus(request.StatusId) == false)
            {
                throw new NotFoundException(ResponseConsts.ProjectNotFound);
            }

            var createProject = new Project
            {
                Name = request.Name,
                Description = request.Description,
                Banner = request.Banner,
                StatusId = request.StatusId,
                CreatedBy = new Guid("e5e65e90-217b-4590-9b1b-23755b27e3c8"),
                UpdatedBy = new Guid("e5e65e90-217b-4590-9b1b-23755b27e3c8")
            };

            await _projectRepository.Create(createProject);

            return ResponseHelper.Create(Map(createProject));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<GenericResponse<ProjectDto>> Update(Guid projectId, UpdateProjectRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GenericResponse<bool>> Remove(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public GenericResponse<ProjectDto?> Get(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public GenericResponse<List<ProjectDto>> Get()
    {
        throw new NotImplementedException();
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
                CreatedAt = project.CreatedAt
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}