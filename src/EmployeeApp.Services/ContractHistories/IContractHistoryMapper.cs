using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Services.Contracts.ContractHistories.Dto;
using Mapster;

namespace EmployeeApp.Services.ContractHistories;

[Mapper]
public interface IContractHistoryMapper
{
    ContractHistoryDto MapTo(ContractHistory item);
}