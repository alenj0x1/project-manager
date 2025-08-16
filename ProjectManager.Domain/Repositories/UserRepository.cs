using ProjectManager.Domain.Context;
using ProjectManager.Domain.Interfaces.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ProjectManager.Domain.Repositories;

public class UserRepository(PostgresContext context) : IUserRepository
{
    private readonly PostgresContext _context = context;
    
    public async Task<User> Create(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public User? Get(Guid userId)
    {
        try
        {
            return _context.Users.FirstOrDefault(x => x.UserId == userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public User? Get(string emailAddress)
    {
        try
        {
            return _context.Users.FirstOrDefault(x => x.EmailAddress == emailAddress);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool IfExistsByIdentification(string identification)
    {
        try
        {
            return _context.Users.Any(x => x.Identification == identification);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool IfExistsByEmailAddress(string emailAddress)
    {
        try
        {
            return _context.Users.Any(x => x.EmailAddress == emailAddress);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IQueryable<User> Queryable()
    {
        try
        {
            return _context.Users.AsQueryable();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User> Update(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Delete(User user)
    {
        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}