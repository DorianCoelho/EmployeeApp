using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Domain.Core.Entities.ContractHistories;

public class ContractHistory : BaseModel
{
    public virtual int Id { get; set; }

    public virtual int ContractId { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public virtual decimal Salary { get; set; }

    public virtual string JobTitle { get; set; } = null!;

    public virtual int WeeklyHours { get; set; }

    public virtual ContractType Type { get; set; }

    public virtual ContractStatus Status { get; set; }

    public virtual WorkDayType WorkDayType { get; set; }

    public virtual DateTime ChangeDate { get; set; } = DateTime.Now;

    public virtual string? Reason { get; set; }
}