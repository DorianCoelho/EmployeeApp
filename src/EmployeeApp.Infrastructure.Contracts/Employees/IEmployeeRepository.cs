using EmployeeApp.Domain.Core.Entities.Employees;

namespace EmployeeApp.Infrastructure.Contracts.Employees;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<List<Employee>> GetAllAsync();

    Task AddAsync(CreateEmployeeFilter employee);
}