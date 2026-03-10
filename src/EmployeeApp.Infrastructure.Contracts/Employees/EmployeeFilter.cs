namespace EmployeeApp.Infrastructure.Contracts.Employees;

public class EmployeeFilter : BaseFilter
{
    public int? Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}