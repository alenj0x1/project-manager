using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class ProjectService : IProjectService
    {
        private List<ProjectDto> _projects = [];

        public GenericResponse<ProjectDto> Create(CreateProjectRequest request)
        {
            try
            {
                var getProject = _projects.Where(proj => proj.Name == request.Name).FirstOrDefault();
                if (getProject is not null)
                {
                    throw new Exception("Proyecto creado previamente");
                }

                var createProject = new ProjectDto
                {
                    ProjectId = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    StatusId = request.StatusId,
                    Banner = request.Banner,
                    CreatedAt = DateTime.Now
                };

                _projects.Add(createProject);

                return new GenericResponse<ProjectDto>
                {
                    Data = createProject,
                    Message = "Proyecto creado exitosamente",
                    StatusCode = 201
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GenericResponse<bool> Delete(Guid projectId)
        {
            try
            {
                // ProyectoDto lo deben obtener de la Lista, usando FirstOrDefault()
                // _projects.Remove(ProyectoDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GenericResponse<ProjectDto?> GetById(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public GenericResponse<ProjectDto> Update(Guid projectId, UpdateProjectRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
