using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Services.Contracts.ContractHistories;
using EmployeeApp.Services.Contracts.ContractHistories.Dto;
using Mapster;

namespace EmployeeApp.Services.ContractHistories;

public class ContractHistoryConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ContractHistory, ContractHistoryDto>();
    }
}