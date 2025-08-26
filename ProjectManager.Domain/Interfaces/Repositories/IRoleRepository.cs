using ProjectManager.Domain.Context;

namespace ProjectManager.Domain.Interfaces.Repositories;

public interface IRoleRepository
{
    bool IfExists(int roleId);
    Role? Get(int roleId);
}