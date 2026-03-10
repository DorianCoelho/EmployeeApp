using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Domain.Core.Entities.Employees;

public class Employee :  BaseModel
{
    public virtual int Id { get; set; }

    public virtual string FirstName { get; set; } = null!;

    public virtual string LastName { get; set; } = null!;
    
    public virtual string Email { get; set; } = null!;

    public virtual string PhoneNumber { get; set; } = null!;

    public virtual string Address { get; set; } = null!;

    public virtual string City { get; set; } = null!;

    public virtual string CassNumber { get; set; } = null!;
    
    public virtual IList<Contract> Contracts { get; set; } = new List<Contract>();
}