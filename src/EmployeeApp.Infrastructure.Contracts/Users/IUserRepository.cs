namespace EmployeeApp.Infrastructure.Contracts.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}