using EmployeeApp.Services.Contracts.Users.Dto;
using EmployeeApp.Services.Users;

namespace EmployeeApp.Services.Users
{
    public partial class UserMapper : IUserMapper
    {
        public UserDto MapTo(User p1)
        {
            return p1 == null ? null : new UserDto()
            {
                Id = p1.Id,
                Email = p1.Email,
                IsActive = p1.IsActive
            };
        }
    }
}