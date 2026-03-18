using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Services.Contracts.ContractHistories;
using EmployeeApp.Services.Contracts.ContractHistories.Dto;
using EmployeeApp.Services.Contracts.ContractHistories.Request;
using MapsterMapper;

namespace EmployeeApp.Services.ContractHistories;

public class ContractHistoryService(IContractUnitOfWork unitOfWork, IMapper mapper) : IContractHistoryService
{
    private readonly IContractUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task CreateContractHistoryAsync(CreateContractHistoryRequest request)
    {
        var filter = _mapper.Map<CreateContractHistoryFilter>(request);
        await _unitOfWork.ContractHistory.CreateContractHistoryAsync(filter);
    }

    public async Task<IEnumerable<ContractHistoryDto>> GetByContractIdAsync(int contractId)
    {
        var contractHistories = await _unitOfWork.ContractHistory.GetByContractIdAsync(contractId);
        return _mapper.Map<IEnumerable<ContractHistoryDto>>(contractHistories);
    }
}