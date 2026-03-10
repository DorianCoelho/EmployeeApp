using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Services.Contracts.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Dto;
using EmployeeApp.Services.Contracts.Employees;
using Mapster;

namespace EmployeeApp.Services.Contracts;

public class ContractConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Contract, ContractDto>();
    }
}