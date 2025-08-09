using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Task;
using ProjectManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class TaskService(Repository<TaskDto> repositoryTasks, Repository<ProjectDto> repositoryProjects) : ITaskService
    {
        private readonly Repository<TaskDto> _repositoryTasks = repositoryTasks;
        private readonly Repository<ProjectDto> _repositoryProjects = repositoryProjects;

        public GenericResponse<ProjectDto> Create(CreateTaskRequest request, Guid projectId)
        {
            try
            {
                var getProject = _repositoryProjects.Queryable().Where(x => x.ProjectId == projectId).FirstOrDefault();
                if (getProject is null)
                {
                    throw new Exception("El proyecto no existe");
                }

                var createTask = new TaskDto
                {
                    TaskId = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    StatusId = request.StatusId
                };

                getProject.Tasks.Add(createTask);

                _repositoryProjects.Update(getProject);

                return new GenericResponse<ProjectDto>()
                {
                    Data = getProject,
                    Message = ResponseConsts.TaskCreated(getProject.Name)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GenericResponse<ProjectDto> Remove(Guid projectId, Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}
