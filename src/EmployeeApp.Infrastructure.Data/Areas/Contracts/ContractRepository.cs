using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Infrastructure.Contracts.Contracts;

namespace EmployeeApp.Infrastructure.Data.Areas.Contracts;

public class ContractRepository : IContractRepository
{

    public async Task<Contract> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(Contract entity)
    {
        throw new NotImplementedException();
    }
}