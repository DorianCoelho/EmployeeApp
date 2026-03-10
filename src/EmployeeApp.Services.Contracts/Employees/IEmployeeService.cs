using EmployeeApp.Services.Contracts.Employees.Dto;
using EmployeeApp.Services.Contracts.Employees.Request;

namespace EmployeeApp.Services.Contracts.Employees;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllAsync();
    
    Task<EmployeeDto> GetByIdAsync(int id);


    Task AddAsync(CreateEmployeeRequest employee);
}