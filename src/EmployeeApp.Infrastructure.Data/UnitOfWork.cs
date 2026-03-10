using EmployeeApp.Domain.Core.Entities;
using NHibernate;

namespace EmployeeApp.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork 
{
    private readonly ISession _session;
    private ITransaction? _transaction;

    public UnitOfWork(ISession session)
    {
        _session = session;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = _session.BeginTransaction();
        await Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        try
        {
            
            await _session.FlushAsync();
            
            if (_transaction != null && _transaction.IsActive)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            if (_transaction != null && _transaction.IsActive)
            {
                await _transaction.RollbackAsync();
            }
            throw; 
        }
        finally
        {
            _transaction?.Dispose();
        }
    }
}