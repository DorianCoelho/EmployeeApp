namespace EmployeeApp.Domain.Core.Entities;

public interface IUnitOfWork
{
    Task SaveAsync();
}