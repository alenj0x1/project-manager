using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;
using ProjectManager.Application.Models.Requests.Task;
using System.Security.Claims;

namespace ProjectManager.Application.Interfaces.Services;

public interface IProjectService
{
    // Project
    Task<GenericResponse<ProjectDto>> Create(Claim claim, CreateProjectRequest request);
    Task<GenericResponse<ProjectDto>> Update(Guid projectId, Claim claim,  UpdateProjectRequest request);
    Task<GenericResponse<bool>> Remove(Claim claim, Guid projectId);
    GenericResponse<ProjectDto?> Get(Guid projectId);
    GenericResponse<List<ProjectDto>> Get();
    // Project Member
    Task<GenericResponse<ProjectDto>> AddMember(Claim claim, Guid projectId, Guid userId);
    Task<GenericResponse<ProjectDto>> RemoveMember(Claim claim, Guid projectId, Guid userId);
    // Task
    GenericResponse<ProjectDto> CreateTask(Guid projectId, Claim claim, CreateTaskRequest request);
    GenericResponse<ProjectDto> ChangeTaskStatus(Guid projectId, int taskId, Claim claim, int statusId);
    GenericResponse<ProjectDto> RemoveTask(Guid projectId, int taskId, Claim claim);
    // Task Member
    GenericResponse<ProjectDto> AssignTaskToMember(Guid projectId, int taskId, Claim claim, Guid userAssignId);
    GenericResponse<ProjectDto> UnassignTaskToMember(Guid projectId, int taskId, Claim claim, Guid userAssignId);
}