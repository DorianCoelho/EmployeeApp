using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts.Employees;
using NHibernate;
using NHibernate.Linq;

namespace EmployeeApp.Infrastructure.Data.Areas.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ISession _session;

    public EmployeeRepository(ISession session)
    {
        _session = session;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        return await _session.GetAsync<Employee>(id);
    }

    public async Task SaveAsync(Employee entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> UpdateAsync(Employee entity)
    {
        var existing = await _session.GetAsync<Employee>(entity.Id);
        if (existing is null)
            throw new KeyNotFoundException($"Employee with ID {entity.Id} not found.");


        existing.FirstName = entity.FirstName;
        existing.LastName = entity.LastName;
        existing.Email = entity.Email;
        existing.PhoneNumber = entity.PhoneNumber;
        existing.CassNumber = entity.CassNumber;

        await _session.FlushAsync();

        return existing;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _session.Query<Employee>()
            .FetchMany(e => e.Contracts)
            .OrderBy(e => e.LastName)
            .ToListAsync();
    }

    public async Task AddAsync(CreateEmployeeFilter request)
    {
        var entity = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            City = request.City,
            CassNumber = request.CassNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        using (var transaction = _session.BeginTransaction())
        {
            try
            {
                await _session.SaveAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}