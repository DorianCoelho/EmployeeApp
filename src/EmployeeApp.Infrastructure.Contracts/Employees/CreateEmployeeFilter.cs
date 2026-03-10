namespace EmployeeApp.Infrastructure.Contracts.Employees;

public class CreateEmployeeFilter
{
    public virtual string FirstName { get; set; } = null!;

    public virtual string LastName { get; set; } = null!;
    
    public virtual string Email { get; set; } = null!;

    public virtual string PhoneNumber { get; set; } = null!;

    public virtual string Address { get; set; } = null!;

    public virtual string City { get; set; } = null!;

    public virtual string CassNumber { get; set; } = null!;
}