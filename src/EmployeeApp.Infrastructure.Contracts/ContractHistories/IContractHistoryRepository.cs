using EmployeeApp.Domain.Core.Entities.ContractHistories;

namespace EmployeeApp.Infrastructure.Contracts.ContractHistories;

public interface IContractHistoryRepository : IGenericRepository<ContractHistory>
{
    Task<ContractHistory> CreateContractHistoryAsync(CreateContractHistoryFilter filter);

    Task<List<ContractHistory>> GetByContractIdAsync(int contractId);
}