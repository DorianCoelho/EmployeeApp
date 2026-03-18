using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using NHibernate;
using NHibernate.Linq;

namespace EmployeeApp.Infrastructure.Data.Areas.ContractHistories;

public class ContractHistoryRepository(ISession session) : IContractHistoryRepository
{
    public async Task<ContractHistory> GetByIdAsync(int id)
    {
        return await session.GetAsync<ContractHistory>(id);
    }

    public async Task SaveAsync(ContractHistory entity)
    {
        await session.SaveOrUpdateAsync(entity);
    }

    public async Task<List<ContractHistory>> GetByContractIdAsync(int contractId)
    {
        return await session.Query<ContractHistory>()
            .Where(h => h.Contract.Id == contractId)
            .OrderByDescending(h => h.ChangeDate)
            .ToListAsync();
    }

    public async Task<ContractHistory> CreateContractHistoryAsync(CreateContractHistoryFilter filter)
    {
        // LoadAsync: no hace SELECT ahora, solo crea proxy.
        // Si el ContractId no existe, explotará en flush/commit (FK).
        var contractProxy = await session.LoadAsync<Contract>(filter.ContractId);

        var now = DateTime.UtcNow;

        var history = new ContractHistory
        {
            Contract = contractProxy,
            Salary = filter.Salary,
            JobTitle = filter.JobTitle,
            WeeklyHours = filter.WeeklyHours,
            Type = filter.Type,
            Status = filter.Status,
            WorkDayType = filter.WorkDayType,
            ChangeDate = filter.ChangeDate == default ? now : filter.ChangeDate.ToUniversalTime(),
            Reason = string.IsNullOrWhiteSpace(filter.Reason) ? null : filter.Reason.Trim(),
            CreatedAt = now,
            UpdatedAt = now
        };

        await session.SaveAsync(history);
        return history;
    }
}