using ProjectManager.Domain.Context;

namespace ProjectManager.Domain.Interfaces.Repositories;

public interface IProjectRepository
{
    // Project
    Task<Project> Create(Project project);
    Task<Project> Update(Project project);
    System.Threading.Tasks.Task Delete(Project project);
    Project? Get(Guid projectId);
    IQueryable<Project> Queryable();

    // Project status
    bool IfExistsProjectStatus(int projectStatusId);
}