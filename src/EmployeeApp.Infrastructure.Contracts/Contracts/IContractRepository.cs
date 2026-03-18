using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Infrastructure.Contracts.Contracts;

public interface IContractRepository : IGenericRepository<Contract>
{
    new Task<Contract> GetByIdAsync(int id);

    Task<List<Contract>> GetContractListByEmployeeAsync(int employeeId);

    Task<Contract> CreateContractAsync(CreateContractFilter filter);

    Task<Contract> UpdateContractAsync(Contract filter);
}