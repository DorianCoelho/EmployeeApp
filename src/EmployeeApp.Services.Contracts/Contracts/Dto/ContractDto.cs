using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Services.Contracts.ContractHistories.Dto;
using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;

namespace EmployeeApp.Services.Contracts.Contracts.Dto;

public class ContractDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string JobTitle { get; set; } = null!;

    public decimal Salary { get; set; }
    public int WeeklyHours { get; set; }

    public ContractType Type { get; set; }
    public ContractStatus Status { get; set; }

    public WorkDayType WorkDayType { get; set; }

    public bool IsCurrentlyActive => Status == ContractStatus.Active &&
                                     (EndDate == null || EndDate >= DateTime.Now);

    //public  IList<ContractHistoryDto> History { get; set; } = new List<ContractHistoryDto>();
}