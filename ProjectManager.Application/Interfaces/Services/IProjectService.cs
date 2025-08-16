using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;

namespace ProjectManager.Application.Interfaces.Services;

public interface IProjectService
{
    Task<GenericResponse<ProjectDto>> Create(CreateProjectRequest request);
    Task<GenericResponse<ProjectDto>> Update(Guid projectId, UpdateProjectRequest request);
    Task<GenericResponse<bool>> Remove(Guid projectId);
    GenericResponse<ProjectDto?> Get(Guid projectId);
    GenericResponse<List<ProjectDto>> Get();
}