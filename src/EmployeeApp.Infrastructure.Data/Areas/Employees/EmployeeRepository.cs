using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts.Employees;
using NHibernate;
using NHibernate.Linq;

namespace EmployeeApp.Infrastructure.Data.Areas.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ISession  _session;

    public EmployeeRepository(ISession session)
    {
        _session = session;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(Employee entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _session.Query<Employee>()
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
            //IsActive = true // Propiedad que definimos antes
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