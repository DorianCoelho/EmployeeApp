using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Infrastructure.Contracts.Users;
using NHibernate;

namespace EmployeeApp.Infrastructure.Data;

public class ContractUnitOfWork : IContractUnitOfWork
{
    private readonly ISession _session;
    private ITransaction? _tx;

    public ContractUnitOfWork(
        ISession session,
        IContractRepository contracts,
        IEmployeeRepository employees,
        IContractHistoryRepository history,
        IUserRepository users)
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

    public async Task BeginAsync()
    {
        if (_tx is null)
            _tx = _session.BeginTransaction();
        await Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        if (_tx is null)
            return;

        try
        {
            await _session.FlushAsync();
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