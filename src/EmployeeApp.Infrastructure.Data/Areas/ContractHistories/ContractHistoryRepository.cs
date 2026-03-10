using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;

namespace EmployeeApp.Infrastructure.Data.Areas.ContractHistories;

public class ContractHistoryRepository : IContractHistoryRepository
{
    public async Task<ContractHistory> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(ContractHistory entity)
    {
        throw new NotImplementedException();
    }
}