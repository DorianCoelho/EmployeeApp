using EmployeeApp.Infrastructure.Contracts;
using NHibernate;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.Users;

namespace EmployeeApp.Infrastructure.Data;

public class ContractUnitOfWork : IContractUnitOfWork
{
    private readonly ISession _session;

    public ContractUnitOfWork(
        ISession session, 
        IContractRepository contracts,
        IEmployeeRepository employees,
        IContractHistoryRepository history, IUserRepository users)
    {
        _session = session;
        Contracts = contracts;
        Employees = employees;
        ContractHistory = history;
        Users = users;
    }

    public IContractRepository Contracts { get; }
    public IEmployeeRepository Employees { get; }
    public IContractHistoryRepository ContractHistory { get; }
    public IUserRepository Users { get; }

    public async Task SaveAsync()
    {
        using var transaction = _session.BeginTransaction();
        try 
        {
            await _session.FlushAsync();
            await transaction.CommitAsync();
        }
        catch 
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}