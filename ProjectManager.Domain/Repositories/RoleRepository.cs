using Microsoft.Extensions.Logging;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Interfaces.Repositories;

namespace ProjectManager.Domain.Repositories;

public class RoleRepository(PostgresContext context, ILogger<IRoleRepository> logger) : IRoleRepository
{
    private readonly PostgresContext _context = context;
    private readonly ILogger<IRoleRepository> _logger = logger;

    public Role? Get(int roleId)
    {
        try
        {
            return _context.Roles.FirstOrDefault(x => x.RoleId == roleId);
        }
        catch (Exception e)
        {
            _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
            throw;
        }
    }

    public bool IfExists(int roleId)
    {
        try
        {
            return _context.Roles.Any(x => x.RoleId == roleId);
        }
        catch (Exception e)
        {
            _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
            throw;
        }
    }
}