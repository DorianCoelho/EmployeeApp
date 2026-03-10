using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Areas.Employees.Pages;

[Authorize]
public class EmployeeList : PageModel
{
    
    private readonly IEmployeeService _employeeService;

    public EmployeeList(IEmployeeService service)
    {
        _employeeService = service;
    }

    public IList<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        
        Employees = await _employeeService.GetAllAsync();

        return Page();
    }
   
}