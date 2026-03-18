using EmployeeApp.Services.Contracts.Contracts.Dto;

namespace EmployeeApp.Services.Contracts.Employees.Dto;

public class EmployeeDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string CassNumber { get; set; } = null!;
}