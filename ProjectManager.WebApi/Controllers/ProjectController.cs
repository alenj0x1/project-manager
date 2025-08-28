using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;
using ProjectManager.Application.Models.Requests.Task;
using ProjectManager.Utils;
using System.Security.Claims;

namespace ProjectManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService, ILogger<ProjectController> logger) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;
        private readonly ILogger<ProjectController> _logger = logger;

        // Project
        [HttpPost("create")]
        [Authorize(Roles = UserTypeConsts.Administrator)]
        public async Task<GenericResponse<ProjectDto>> Create([FromBody] CreateProjectRequest request)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return await _projectService.Create(claim, request);
            }
            catch (Exception e)
            {
                _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        [HttpPut("update/{projectId:guid}")]
        [Authorize(Roles = UserTypeConsts.Administrator)]
        public async Task<GenericResponse<ProjectDto>> Update(Guid projectId, [FromBody] UpdateProjectRequest request)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return await _projectService.Update(projectId, claim, request);
            }
            catch (Exception e)
            {
                _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        [HttpDelete("remove/{projectId:guid}")]
        [Authorize(Roles = UserTypeConsts.Administrator)]
        public async Task<GenericResponse<bool>> Remove(Guid projectId)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return await _projectService.Remove(claim, projectId);
            }
            catch (Exception e)
            {
                _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        [HttpGet("getById/{projectId:guid}")]
        [Authorize]
        public GenericResponse<ProjectDto> GetById(Guid projectId)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return _projectService.Get(projectId);
            }
            catch (Exception e)
            {
                _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        [HttpGet("getAll")]
        [Authorize]
        public GenericResponse<List<ProjectDto>> GetAll([FromQuery] GetProjectRequest request)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return _projectService.Get(request);
            }
            catch (Exception e)
            {
                _logger.LogWarning("{ExceptionMessage} {ExceptionStackTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        // Project members
        [HttpPut("{projectId:guid}/members/add/{userId:guid}")]
        [Authorize(Roles = UserTypeConsts.Administrator)]
        public async Task<GenericResponse<ProjectDto>> AddMember(Guid projectId, Guid userId)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return await _projectService.AddMember(claim, projectId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{projectId:guid}/members/remove/{userId:guid}")]
        [Authorize(Roles = UserTypeConsts.Administrator)]
        public async Task<GenericResponse<ProjectDto>> RemoveMember(Guid projectId, Guid userId)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return await _projectService.RemoveMember(claim, projectId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("{projectId:guid}/tasks/create")]
        [Authorize(Roles = UserTypeConsts.Administrator)]
        public async Task<GenericResponse<ProjectDto>> CreateTask(Guid projectId, [FromBody] CreateTaskRequest request)
        {
            try
            {
                var claim = User.FindFirst("UserId")
                    ?? throw new UnauthorizedAccessException(ResponseConsts.UserIdentityNotFound);

                return await _projectService.CreateTask(projectId, claim, request);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
