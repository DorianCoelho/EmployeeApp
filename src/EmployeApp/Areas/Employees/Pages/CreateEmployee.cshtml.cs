using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Request;
using EmployeeApp.Services.Contracts.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Areas.Employees.Pages;

[Authorize]
public class CreateEmployee(IEmployeeService employeeService) : PageModel
{
    [BindProperty] public CreateEmployeeRequest Employee { get; set; } = new();

    public async Task<IActionResult> OnPostAsync([FromServices] IValidator<CreateEmployeeRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(Employee);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, requestName: nameof(Employee));
            return Page();
        }


        await employeeService.AddAsync(Employee);

        return RedirectToPage("EmployeeList", new { area = "Employees" });
    }
}