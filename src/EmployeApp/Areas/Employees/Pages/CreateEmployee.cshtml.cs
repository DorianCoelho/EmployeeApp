using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Areas.Employees.Pages;

[Authorize]
public class CreateEmployee : PageModel
{
    private readonly IEmployeeService _employeeService;

    public CreateEmployee(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [BindProperty]
    public CreateEmployeeRequest Employee { get; set; } = new();
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _employeeService.AddAsync(Employee);

        return RedirectToPage("EmployeeList", new {area = "Employees"});

    }
}