using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;
using ProjectManager.Application.Models.Requests.Task;
using System.Security.Claims;

namespace ProjectManager.Application.Interfaces.Services;

public interface IProjectService
{
    // Project
    Task<GenericResponse<ProjectDto>> CreateAsync(Claim claim, CreateProjectRequest request);
    Task<GenericResponse<ProjectDto>> UpdateAsync(Guid projectId, Claim claim,  UpdateProjectRequest request);
    Task<GenericResponse<bool>> RemoveAsync(Claim claim, Guid projectId);
    GenericResponse<ProjectDto> Get(Guid projectId);
    GenericResponse<List<ProjectDto>> Get(GetProjectRequest request);
    // Project Member
    Task<GenericResponse<ProjectDto>> AddMemberAsync(Claim claim, Guid projectId, Guid userId);
    Task<GenericResponse<ProjectDto>> RemoveMemberAsync(Claim claim, Guid projectId, Guid userId);
    // Task
    Task<GenericResponse<ProjectDto>> CreateTaskAsync(Guid projectId, Claim claim, CreateTaskRequest request);
    Task<GenericResponse<ProjectDto>> ChangeTaskStatusAsync(Guid projectId, Guid taskId, Claim claim, int statusId);
    Task<GenericResponse<ProjectDto>> RemoveTaskAsync(Guid projectId, Guid taskId, Claim claim);
    // Task Member
    Task<GenericResponse<ProjectDto>> AssignTaskToMemberAsync(Guid projectId, Guid taskId, Claim claim, Guid userAssignId);
    Task<GenericResponse<ProjectDto>> UnassignTaskToMemberAsync(Guid projectId, Guid taskId, Claim claim);
}