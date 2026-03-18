using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Infrastructure.Contracts.Users;

namespace EmployeeApp.Infrastructure.Contracts;

public interface IContractUnitOfWork
{
    IContractRepository Contracts { get; }
    IEmployeeRepository Employees { get; }
    IContractHistoryRepository ContractHistory { get; }
    IUserRepository Users { get; }

    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();
}