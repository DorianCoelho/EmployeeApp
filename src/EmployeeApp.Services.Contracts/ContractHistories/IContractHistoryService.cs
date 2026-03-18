using EmployeeApp.Services.Contracts.ContractHistories.Dto;

namespace EmployeeApp.Services.Contracts.ContractHistories;

public interface IContractHistoryService
{
    Task<IEnumerable<ContractHistoryDto>> GetByContractIdAsync(int contractId);
}