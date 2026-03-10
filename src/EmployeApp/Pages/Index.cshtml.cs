using EmployeeApp.Services.Contracts.Users;
using EmployeeApp.Services.Contracts.Users.Request;
using EmployeeApp.Services.Contracts.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeApp.Pages;

public class IndexModel : PageModel
{
    private readonly IUserService _userService;

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty] public AuthRequest AuthRequest { get; set; }
    
    public async Task<IActionResult> OnPostAsync([FromServices] IValidator<AuthRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(AuthRequest);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, requestName: nameof(AuthRequest));
            return Page();
        }
        
        bool isAuthenticated = await _userService.AuthenticateAsync(email:AuthRequest.Email, password:AuthRequest.Password);
        if (!isAuthenticated)
        {
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return Page();
        }
        
        return RedirectToPage("EmployeeList", new {area = "Employees"});
    }
}