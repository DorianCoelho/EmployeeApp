using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Services.Contracts.Contracts.Dto;
using Mapster;

namespace EmployeeApp.Services.Contracts;

[Mapper]
public interface IContractMapper
{
    ContractDto MapTo(ContractHistory item);
}