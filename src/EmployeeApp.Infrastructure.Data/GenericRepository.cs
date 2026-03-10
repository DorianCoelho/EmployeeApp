using EmployeeApp.Domain.Core.Entities;
using EmployeeApp.Infrastructure.Contracts;
using NHibernate;

namespace EmployeeApp.Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected readonly ISession _session;

    public GenericRepository(ISession session)
    {
        _session = session;
    }

    public async Task<T> GetByIdAsync(int id) => await _session.GetAsync<T>(id);

    public async Task SaveAsync(T entity)
    {
        await _session.SaveOrUpdateAsync(entity);
        await _session.FlushAsync(); // Sincroniza con DB
    }
    
    // El resto de métodos (Delete, GetAll, etc.)
}