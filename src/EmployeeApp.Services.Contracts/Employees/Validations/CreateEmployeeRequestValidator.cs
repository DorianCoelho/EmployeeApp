using EmployeeApp.Services.Contracts.Employees.Request;
using FluentValidation;

namespace EmployeeApp.Services.Contracts.Employees.Validations;

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.CassNumber)
            .NotEmpty().WithMessage("CASS number is required.")
            .Matches(@"^\d{6}[A-Z]$")
            .WithMessage("CASS number must have 6 digits followed by 1 uppercase letter (e.g., 000000A).");
    }
}