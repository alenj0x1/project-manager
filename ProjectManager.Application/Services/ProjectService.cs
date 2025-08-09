using Microsoft.Extensions.Configuration;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Project;
using ProjectManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class ProjectService(Repository<ProjectDto> repositoryProjects) : IProjectService
    {
        private readonly Repository<ProjectDto> _repositoryProjects = repositoryProjects;

        public GenericResponse<ProjectDto> Create(CreateProjectRequest request)
        {
            try
            {
                var getProject = _repositoryProjects.Queryable().Where(proj => proj.Name == request.Name).FirstOrDefault();
                if (getProject is not null)
                {
                    throw new Exception(ResponseConsts.ProjectNotFound);
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

                _repositoryProjects.Add(createProject);

                return new GenericResponse<ProjectDto>
                {
                    Data = createProject,
                    Message = ResponseConsts.ProjectCreated,
                    StatusCode = ResponseHttpCodes.Created
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
                var getProject = _repositoryProjects.Queryable().Where(proj => proj.ProjectId == projectId).FirstOrDefault();
                if (getProject is null)
                {
                    throw new Exception(ResponseConsts.ProjectNotFound);
                }

                _repositoryProjects.Remove(getProject);

                return new GenericResponse<bool>
                {
                   Data = true,
                   Message = ResponseConsts.ProjectDeleted
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GenericResponse<ProjectDto?> GetById(Guid projectId)
        {
            try
            {
                var getProject = _repositoryProjects.Queryable().Where(proj => proj.ProjectId == projectId).FirstOrDefault();

                return new GenericResponse<ProjectDto?>
                {
                    Data = getProject,
                    Message = ResponseConsts.ProjectSearchCompleted
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GenericResponse<ProjectDto> Update(Guid projectId, UpdateProjectRequest request)
        {
            try
            {
                var getProject = _repositoryProjects.Queryable().Where(proj => proj.ProjectId == projectId).FirstOrDefault();
                if (getProject is null)
                {
                    throw new Exception(ResponseConsts.ProjectNotFound);
                }

                getProject.Name = request.Name ?? getProject.Name;
                getProject.Banner = request.Banner ?? getProject.Banner;
                getProject.Description = request.Description ?? getProject.Description;
                getProject.StatusId = request.StatusId ?? getProject.StatusId;

                _repositoryProjects.Update(getProject);

                return new GenericResponse<ProjectDto>
                {
                    Data = getProject,
                    Message = ResponseConsts.ProjectUpdated
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
