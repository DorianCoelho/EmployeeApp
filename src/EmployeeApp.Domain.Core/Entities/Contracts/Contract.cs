using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Domain.Core.Entities.Employees;

namespace EmployeeApp.Domain.Core.Entities.Contracts;

public class Contract : BaseModel
{
    public virtual int Id { get; set; }

    public virtual int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual DateTime StartDate { get; set; }

    public virtual DateTime? EndDate { get; set; }

    public virtual string JobTitle { get; set; } = null!;

    public virtual decimal Salary { get; set; }
    public virtual int WeeklyHours { get; set; }

    public virtual ContractType Type { get; set; }
    public virtual ContractStatus Status { get; set; }

    public virtual WorkDayType WorkDayType { get; set; }

    public virtual bool IsCurrentlyActive => Status == ContractStatus.Active &&
                                             (EndDate == null || EndDate >= DateTime.Now);

    public virtual IList<ContractHistory> History { get; set; } = new List<ContractHistory>();
}