using ProjectManager.Domain.Context;

namespace ProjectManager.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> Create(User user);
    User? Get(Guid userId);
    User? Get(string emailAddress);
    bool IfExistsByIdentification(string identification);
    bool IfExistsByEmailAddress(string emailAddress);
    IQueryable<User> Queryable();
    Task<User> Update(User user);
    System.Threading.Tasks.Task Delete(User user);
}