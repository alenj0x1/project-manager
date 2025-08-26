using ProjectManager.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<ProjectManager.Domain.Context.Task> Create(ProjectManager.Domain.Context.Task task);
        ProjectManager.Domain.Context.Task? Get(Guid projectId, Guid taskId);
        bool IfTaskIsInFromProject(Guid projectId, Guid taskId);
        bool IfTaskIsFromMember(Guid projectId, Guid taskId, Guid userId);
        bool IfExistsTaskStatus(int taskStatusId);
        Task<ProjectManager.Domain.Context.Task> Update(ProjectManager.Domain.Context.Task task);
        System.Threading.Tasks.Task Remove(ProjectManager.Domain.Context.Task task);
    }
}
