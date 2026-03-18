using System.Globalization;
using EmployeeApp.Services.Contracts;
using EmployeeApp.Services.Contracts.ContractHistories.Dto;
using EmployeeApp.Services.Contracts.Employees.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Areas.Contracts.Pages;

[Authorize]
public class ContractHistory(IUnitOfService unitOfService) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "contractId")]
    public int ContractId { get; set; }

    public int EmployeeId { get; set; }
    public string? EmployeeName { get; set; }

    public CultureInfo EuroCulture { get; } = CultureInfo.GetCultureInfo("es-ES");

    public List<ContractHistoryDto> History { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var contract = await unitOfService.ContractService.GetByIdAsync(ContractId);
        if (contract is null)
            return NotFound();

        EmployeeId = contract.Employee.Id;


        EmployeeDto employee = await unitOfService.EmployeeService.GetByIdAsync(EmployeeId);
        EmployeeName = $"{employee.FirstName} {employee.LastName}";


        var history = await unitOfService.ContractHistoryService.GetByContractIdAsync(ContractId);

        History = history
            .OrderByDescending(x => x.ChangeDate)
            .Select(x => new ContractHistoryDto()
            {
                Id = x.Id,
                Salary = x.Salary,
                JobTitle = x.JobTitle,
                WeeklyHours = x.WeeklyHours,
                Type = x.Type,
                Status = x.Status,
                WorkDayType = x.WorkDayType,
                ChangeDate = x.ChangeDate,
                Reason = x.Reason
            })
            .ToList();

        return Page();
    }
}