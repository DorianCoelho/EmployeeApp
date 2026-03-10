using EmployeeApp.Services.Contracts.Users.Dto;
using Mapster;

namespace EmployeeApp.Services.Users;

public class UserConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>();
    }
}