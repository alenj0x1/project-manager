using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Task;

namespace ProjectManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService tasksService) : ControllerBase
    {
        private readonly ITaskService _tasksService = tasksService;

        [HttpPost("create/{projectId:guid}")]
        public GenericResponse<ProjectDto> Create([FromBody] CreateTaskRequest request, Guid projectId)
        {
            try
            {
                return _tasksService.Create(request, projectId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("remove/{projectId:guid}/{taskId:guid}")]
        public GenericResponse<ProjectDto> Remove(Guid projectId, Guid taskId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
