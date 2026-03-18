using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Infrastructure.Contracts.Users;
using NHibernate;

namespace EmployeeApp.Infrastructure.Data;

public class ContractUnitOfWork(
    ISession session,
    IContractRepository contracts,
    IEmployeeRepository employees,
    IContractHistoryRepository history,
    IUserRepository users)
    : IContractUnitOfWork
{
    private ITransaction? _tx;

    public IContractRepository Contracts { get; } = contracts;
    public IEmployeeRepository Employees { get; } = employees;
    public IContractHistoryRepository ContractHistory { get; } = history;
    public IUserRepository Users { get; } = users;

    public async Task BeginAsync()
    {
        if (_tx is null)
            _tx = session.BeginTransaction();
        await Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        if (_tx is null)
            return;

        try
        {
            await session.FlushAsync();
            await _tx.CommitAsync();
        }
        finally
        {
            _tx.Dispose();
            _tx = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_tx is null)
            return;

        try
        {
            await _tx.RollbackAsync();
        }
        finally
        {
            _tx.Dispose();
            _tx = null;
        }
    }
}