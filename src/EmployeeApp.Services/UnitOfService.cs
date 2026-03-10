using EmployeeApp.Services.Contracts;
using EmployeeApp.Services.Contracts.ContractHistories;
using EmployeeApp.Services.Contracts.Contracts;
using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Users;

namespace EmployeeApp.Services;

public class UnitOfService : IUnitOfService
{
    public UnitOfService(IEmployeeService employeeService, IContractService contractService, IContractHistoryService contractHistoryService, IUserService userService)
    {
        EmployeeService = employeeService;
        ContractService = contractService;
        ContractHistoryService = contractHistoryService;
        UserService = userService;
    }

    public IEmployeeService EmployeeService { get; }
    public IContractService ContractService { get; }
    public IContractHistoryService ContractHistoryService { get; }
    public IUserService UserService { get; }
}