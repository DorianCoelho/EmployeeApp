using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Infrastructure.Contracts.Contracts;

public class EditContractFilter
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }


    public DateTime StartDate { get; set; } = DateTime.Today;


    public DateTime? EndDate { get; set; }


    public string JobTitle { get; set; } = string.Empty;


    public decimal Salary { get; set; }


    public int WeeklyHours { get; set; }


    public ContractType Type { get; set; }


    public ContractStatus Status { get; set; }


    public WorkDayType WorkDayType { get; set; }
}