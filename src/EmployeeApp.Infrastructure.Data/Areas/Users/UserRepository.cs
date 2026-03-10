using EmployeeApp.Infrastructure.Contracts.Users;
using NHibernate;
using NHibernate.Linq;

namespace EmployeeApp.Infrastructure.Data.Areas.Users;

public class UserRepository(ISession session) : IUserRepository
{
    public async Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await session.Query<User>()
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
    }
}