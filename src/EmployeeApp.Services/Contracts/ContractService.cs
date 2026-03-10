using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Services.Contracts.Contracts;
using MapsterMapper;

namespace EmployeeApp.Services.Contracts;

public class ContractService : IContractService
{
    private readonly IContractUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContractService(IContractUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}