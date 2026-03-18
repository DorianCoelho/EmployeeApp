using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Dto;
using Mapster;

namespace EmployeeApp.Services.Contracts;

public class ContractConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Contract, ContractDto>();
    }
}