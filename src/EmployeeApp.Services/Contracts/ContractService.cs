using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using EmployeeApp.Services.Contracts.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Dto;
using EmployeeApp.Services.Contracts.Contracts.Request;
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

    public async Task<List<ContractDto>> GetContractListByEmployeeAsync(int employeeId)
    {
        var contract = await _unitOfWork.Contracts.GetContractListByEmployeeAsync(employeeId);
        return _mapper.Map<List<ContractDto>>(contract);
    }

    public async Task<ContractDto?> GetByIdAsync(int contractId)
    {
        var contract = await _unitOfWork.Contracts.GetByIdAsync(contractId);
        return contract is null ? null : _mapper.Map<ContractDto>(contract);
    }

    public async Task<ContractDto> CreateContractAsync(CreateContractRequest request)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var contractFilter = _mapper.Map<CreateContractFilter>(request);
            var contract = await _unitOfWork.Contracts.CreateContractAsync(contractFilter);

            var historyFilter = new CreateContractHistoryFilter
            {
                ContractId = contract.Id,
                Salary = contract.Salary,
                JobTitle = contract.JobTitle,
                WeeklyHours = contract.WeeklyHours,
                Type = contract.Type,
                Status = contract.Status,
                WorkDayType = contract.WorkDayType,
                ChangeDate = DateTime.UtcNow,
                Reason = "Creación inicial del contrato"
            };

            await _unitOfWork.ContractHistory.CreateContractHistoryAsync(historyFilter);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ContractDto>(contract);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateContractAsync(EditContractRequest request)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var contract = await _unitOfWork.Contracts.GetByIdAsync(request.Id);
            if (contract is null)
                throw new KeyNotFoundException($"Contrato con ID {request.Id} no encontrado.");

            contract.Salary = request.Salary;
            contract.WeeklyHours = request.WeeklyHours;

            await _unitOfWork.Contracts.UpdateContractAsync(contract);

            var historyFilter = new CreateContractHistoryFilter
            {
                ContractId = contract.Id,
                Salary = contract.Salary,
                JobTitle = contract.JobTitle,
                WeeklyHours = contract.WeeklyHours,
                Type = contract.Type,
                Status = contract.Status,
                WorkDayType = contract.WorkDayType,
                ChangeDate = DateTime.UtcNow,
                Reason = request.Reason ?? "Actualización de salario/horas"
            };

            await _unitOfWork.ContractHistory.CreateContractHistoryAsync(historyFilter);
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}