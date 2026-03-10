using EmployeeApp.Services.Contracts.ContractHistories;
using EmployeeApp.Services.Contracts.Contracts;
using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Users;

namespace EmployeeApp.Services.Contracts;

public interface IUnitOfService
{
    IEmployeeService EmployeeService { get; }
    
    IContractService ContractService { get; }
    
    IContractHistoryService ContractHistoryService { get; }
    
    IUserService UserService { get; }
}