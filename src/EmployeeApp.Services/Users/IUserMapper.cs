using EmployeeApp.Services.Contracts.Users.Dto;
using Mapster;

namespace EmployeeApp.Services.Users;

[Mapper]
public interface IUserMapper
{
    UserDto MapTo(User model);
}