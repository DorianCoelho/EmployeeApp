using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using NHibernate;
using NHibernate.Linq;

namespace EmployeeApp.Infrastructure.Data.Areas.Contracts;

public class ContractRepository(ISession session) : IContractRepository
{
    public async Task<Contract> GetByIdAsync(int id)
    {
        return await session.GetAsync<Contract>(id);
    }

    public async Task SaveAsync(Contract entity)
    {
        await session.SaveOrUpdateAsync(entity);
    }

    public async Task<List<Contract>> GetContractListByEmployeeAsync(int employeeId)
    {
        return await session.Query<Contract>()
            .Where(c => c.Employee.Id == employeeId)
            .ToListAsync();
    }

    public async Task<Contract> CreateContractAsync(CreateContractFilter filter)
    {
        var employee = await session.GetAsync<Employee>(filter.EmployeeId);
        if (employee is null)
            throw new KeyNotFoundException($"Employee with ID {filter.EmployeeId} not found.");

        var contract = new Contract
        {
            Employee = employee,
            StartDate = filter.StartDate,
            EndDate = filter.EndDate,
            JobTitle = filter.JobTitle,
            Salary = filter.Salary,
            WeeklyHours = filter.WeeklyHours,
            Type = filter.Type,
            Status = filter.Status,
            WorkDayType = filter.WorkDayType,
        };

        await session.SaveAsync(contract);
        return contract;
    }

    public async Task<Contract> UpdateContractAsync(Contract filter)
    {
        var existing = await session.GetAsync<Contract>(filter.Id);
        if (existing is null)
            throw new KeyNotFoundException($"Contract with ID {filter.Id} not found.");

        existing.Salary = filter.Salary;
        existing.WeeklyHours = filter.WeeklyHours;

        await session.FlushAsync();

        return existing;
    }
}