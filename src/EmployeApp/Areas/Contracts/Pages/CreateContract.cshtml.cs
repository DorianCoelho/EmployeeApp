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
public class CreateContract(IUnitOfService unitOfService) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "employeeId")]
    public int EmployeeId { get; set; }

    [BindProperty] public CreateContractRequest Contract { get; set; } = new();

    public EmployeeDto Employee { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        if (Contract.StartDate == default)
            Contract.StartDate = DateTime.Today;
        Contract.EmployeeId = EmployeeId;

        Employee = await unitOfService.EmployeeService.GetByIdAsync(EmployeeId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([FromServices] IValidator<CreateContractRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(Contract);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, requestName: nameof(Contract));
            return Page();
        }

        await unitOfService.ContractService.CreateContractAsync(Contract);

        return RedirectToPage("/EmployeeCard", new Dictionary<string, string>()
        {
            { "id", EmployeeId.ToString() },
            { "area", "Employees" }
        });
    }
}