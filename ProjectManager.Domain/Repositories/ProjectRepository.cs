using ProjectManager.Domain.Context;
using ProjectManager.Domain.Interfaces.Repositories;

namespace ProjectManager.Domain.Repositories;

public class ProjectRepository(PostgresContext context) : IProjectRepository
{
    private readonly PostgresContext _context = context;

    // Project
    public async Task<Project> Create(Project project)
    {
        try
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync(); // Aquí se guarda nuestro proyecto en base de datos

            return project;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Project> Update(Project project)
    {
        try
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return project;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async System.Threading.Tasks.Task Delete(Project project)
    {
        try
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Project? Get(Guid projectId)
    {
        try
        {
            return _context.Projects.FirstOrDefault(x => x.ProjectId == projectId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IQueryable<Project> Queryable()
    {
        try
        {
            return _context.Projects.AsQueryable();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Project status
    public bool IfExistsProjectStatus(int projectStatusId)
    {
        try
        {
            return _context.ProjectsStatuses.Any(x => x.ProjectStatusId == projectStatusId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}