using ProjectManager.Domain.Context;
using ProjectManager.Domain.Interfaces.Repositories;

namespace ProjectManager.Domain.Repositories;

public class RoleRepository(PostgresContext context) : IRoleRepository
{
    private readonly PostgresContext _context = context;
    
    public bool IfExists(int roleId)
    {
        try
        {
            return _context.Roles.Any(x => x.RoleId == roleId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}