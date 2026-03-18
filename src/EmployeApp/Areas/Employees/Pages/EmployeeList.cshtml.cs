using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Areas.Employees.Pages;

[Authorize]
public class EmployeeList(IEmployeeService service) : PageModel
{
    public IList<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        Employees = await service.GetAllAsync();

        return Page();
    }
}