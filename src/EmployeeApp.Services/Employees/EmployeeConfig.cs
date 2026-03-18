using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;
using EmployeeApp.Services.Contracts.Employees.Request;
using Mapster;

namespace EmployeeApp.Services.Employees;

public class EmployeeConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Employee, EmployeeDto>();

        config.NewConfig<CreateEmployeeRequest, CreateEmployeeFilter>();
    }
}