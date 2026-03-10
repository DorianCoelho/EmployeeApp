using EmployeeApp.Services.Contracts.Users.Dto;

namespace EmployeeApp.Services.Contracts.Users;

public interface IUserService
{
    Task<bool> AuthenticateAsync(string email, string password);
}