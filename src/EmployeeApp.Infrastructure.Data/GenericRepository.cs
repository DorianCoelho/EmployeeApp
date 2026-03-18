using EmployeeApp.Domain.Core.Entities;
using EmployeeApp.Infrastructure.Contracts;
using NHibernate;

namespace EmployeeApp.Infrastructure.Data;

public class GenericRepository<T>(ISession session) : IGenericRepository<T>
    where T : BaseModel
{
    public async Task<T> GetByIdAsync(int id) => await session.GetAsync<T>(id);

    public async Task SaveAsync(T entity)
    {
        await session.SaveOrUpdateAsync(entity);
        await session.FlushAsync();
    }
}