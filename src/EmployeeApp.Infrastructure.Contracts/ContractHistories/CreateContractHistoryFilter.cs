using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Infrastructure.Contracts.ContractHistories;

public class CreateContractHistoryFilter
{
    public int ContractId { get; set; }

    public decimal Salary { get; set; }

    public string JobTitle { get; set; } = string.Empty;

    public int WeeklyHours { get; set; }

    public ContractType Type { get; set; }

    public ContractStatus Status { get; set; }

    public WorkDayType WorkDayType { get; set; }

    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

    public string? Reason { get; set; }
}