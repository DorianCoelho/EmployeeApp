using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;
using EmployeeApp.Services.Contracts.Employees.Request;
using Mapster;

namespace EmployeeApp.Services.Employees;

[Mapper]
public interface IEmployeeMapper
{
    EmployeeDto MapToDto(Employee employee);
    
    CreateEmployeeRequest MapToCreateRequestDto(CreateEmployeeFilter employee);
}