using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Task;

namespace ProjectManager.Application.Interfaces.Services
{
    public interface ITaskService
    {
        GenericResponse<ProjectDto> Create(CreateTaskRequest request, Guid projectId);
        GenericResponse<ProjectDto> Remove(Guid projectId, Guid taskId);
    }
}
