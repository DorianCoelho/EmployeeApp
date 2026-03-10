using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Services.Contracts.ContractHistories;
using MapsterMapper;

namespace EmployeeApp.Services.ContractHistories;

public class ContractHistoryService : IContractHistoryService
{
    private readonly IContractUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContractHistoryService(IContractUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}