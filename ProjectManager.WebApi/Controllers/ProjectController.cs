using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Models.Requests.Project;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Services;

namespace ProjectManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        [HttpPost("create")]
        public GenericResponse<ProjectDto> Create([FromBody] CreateProjectRequest request)
        {
            try
            {
                return _projectService.Create(request);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // getById/ce5c4d95-dd15-4e8b-bb3a-7bf19c46f40a
        [HttpGet("/getById/{projectId:guid}")]
        public GenericResponse<ProjectDto?> GetById(Guid projectId)
        {
            try
            {
                return _projectService.GetById(projectId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Agregar metodo para actualizar un proyecto
        [HttpPut("/update/{projectId:guid}")]
        public GenericResponse<ProjectDto> Update(Guid projectId, UpdateProjectRequest request)
        {
            try
            {
                return _projectService.Update(projectId, request);
            }
            catch (Exception)
            {

                throw;
            }
        }



        // Agregar metodo para eliminar un proyecto
        [HttpDelete("/delete/{projectId:guid}")]
        public GenericResponse<bool> Delete(Guid projectId)
        {
            try
            {
                return _projectService.Delete(projectId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
