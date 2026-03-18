using EmployeeApp.Services.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Dto;
using EmployeeApp.Services.Contracts.Employees.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Areas.Employees.Pages;

[Authorize]
public class EmployeeCard(IUnitOfService service) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "id")]
    public int Id { get; set; }

    public EmployeeDto Employee { get; set; } = null!;

    public List<ContractDto> Contracts { get; set; } = [];


    public async Task<ActionResult> OnGetAsync()
    {
        Employee = await service.EmployeeService.GetByIdAsync(Id);

        Contracts = await service.ContractService.GetContractListByEmployeeAsync(Id);

        return Page();
    }
}