namespace EmployeeApp.Infrastructure.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);

    Task SaveAsync(T entity);
}