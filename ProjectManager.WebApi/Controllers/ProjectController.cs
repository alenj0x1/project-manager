using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebApi.Models.Requests.Project;

namespace ProjectManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] CreateProjectRequest request)
        {
            try
            {
                return Ok(request);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // getById/ce5c4d95-dd15-4e8b-bb3a-7bf19c46f40a
        [HttpGet("/getById/{projectId:guid}/{number:int}")]
        public IActionResult GetById(Guid projectId, int number)
        {
            try
            {
                return Ok(new
                {
                    ProjectId = projectId,
                    Number = number
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Agregar metodo para actualizar un proyecto

        // Agregar metodo para eliminar un proyecto
    }
}
