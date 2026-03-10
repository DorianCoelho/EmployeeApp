using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;
using EmployeeApp.Services.Contracts.Employees.Request;
using MapsterMapper;

namespace EmployeeApp.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IContractUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IContractUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDto>> GetAllAsync()
    {
        var emplyeeList = await _unitOfWork.Employees.GetAllAsync();

        return _mapper.Map<List<EmployeeDto>>(emplyeeList);
    }

    public async Task<EmployeeDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(CreateEmployeeRequest employee)
    {
        CreateEmployeeFilter filter = _mapper.Map<CreateEmployeeFilter>(employee);
        await _unitOfWork.Employees.AddAsync(filter);
    }
}