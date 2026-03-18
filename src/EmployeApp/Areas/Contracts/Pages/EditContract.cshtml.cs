using EmployeeApp.Services.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Request;
using EmployeeApp.Services.Contracts.Employees.Dto;
using EmployeeApp.Services.Contracts.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Areas.Contracts.Pages;

[Authorize]
public class EditContract(IUnitOfService unitOfService) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "id")]
    public int ContractId { get; set; }

    [BindProperty] public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; } = new();

    // Editable
    [BindProperty] public EditContractRequest Edit { get; set; } = new();

    public async Task<IActionResult> InicializeDataAsync()
    {
        var contract = await unitOfService.ContractService.GetByIdAsync(ContractId);
        if (contract is null)
            return NotFound();

        EmployeeId = contract.Employee.Id;
        Employee = await unitOfService.EmployeeService.GetByIdAsync(EmployeeId);


        Edit = new EditContractRequest
        {
            EmployeeId = contract.Employee.Id,
            Id = contract.Id,
            Salary = contract.Salary,
            WeeklyHours = contract.WeeklyHours,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            JobTitle = contract.JobTitle,
            Type = contract.Type,
            Status = contract.Status,
            WorkDayType = contract.WorkDayType
        };

        return Page();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        return await InicializeDataAsync();
    }

    public async Task<IActionResult> OnPostAsync([FromServices] IValidator<EditContractRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(Edit);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, requestName: nameof(Edit));

            // Recargar datos para que la vista se pinte bien tras error
            var contract = await unitOfService.ContractService.GetByIdAsync(ContractId);
            if (contract is null) return NotFound();

            EmployeeId = contract.Employee.Id;
            Employee = await unitOfService.EmployeeService.GetByIdAsync(EmployeeId);

            return await InicializeDataAsync();
        }

        await unitOfService.ContractService.UpdateContractAsync(Edit);

        return RedirectToPage("/EmployeeCard", new Dictionary<string, string>
        {
            { "id", Edit.EmployeeId.ToString() },
            { "area", "Employees" }
        });
    }
}