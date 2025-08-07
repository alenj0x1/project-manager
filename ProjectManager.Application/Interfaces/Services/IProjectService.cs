using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;

namespace ProjectManager.Application.Interfaces.Services
{
    public interface IProjectService
    {
        GenericResponse<ProjectDto> Create(CreateProjectRequest request);
        GenericResponse<ProjectDto?> GetById(Guid projectId);
        GenericResponse<ProjectDto> Update(Guid projectId, UpdateProjectRequest request);
        GenericResponse<bool> Delete(Guid projectId);
    }
}
