using EmployeeApp.Services.Contracts.Contracts.Dto;
using EmployeeApp.Services.Contracts.Contracts.Request;
using EmployeeApp.Services.Contracts.Employees.Dto;

namespace EmployeeApp.Services.Contracts.Contracts;

public interface IContractService
{
    Task<List<ContractDto>> GetContractListByEmployeeAsync(int employeeId);

    Task<ContractDto> CreateContractAsync(CreateContractRequest request);

    Task UpdateContractAsync(EditContractRequest request);

    Task<ContractDto?> GetByIdAsync(int contractId);
}