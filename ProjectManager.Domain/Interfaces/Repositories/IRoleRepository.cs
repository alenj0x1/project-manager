namespace ProjectManager.Domain.Interfaces.Repositories;

public interface IRoleRepository
{
    bool IfExists(int roleId);
}