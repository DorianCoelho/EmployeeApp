using EmployeeApp.Services.Contracts.Contracts.Request;
using FluentValidation;

namespace EmployeeApp.Services.Contracts.Contracts.Validations;

public class CreateContractRequestValidator : AbstractValidator<CreateContractRequest>
{
    public CreateContractRequestValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("El empleado es obligatorio.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("La fecha de inicio es obligatoria.");

        RuleFor(x => x.EndDate)
            .Must((req, endDate) => endDate == null || endDate.Value.Date >= req.StartDate.Date)
            .WithMessage("La fecha de fin no puede ser anterior a la fecha de inicio.");

        RuleFor(x => x.JobTitle)
            .NotEmpty().WithMessage("El puesto de trabajo es obligatorio.")
            .MaximumLength(150).WithMessage("El puesto de trabajo no puede superar los 150 caracteres.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("El salario debe ser mayor que 0.");

        RuleFor(x => x.WeeklyHours)
            .GreaterThan(0).WithMessage("Las horas semanales deben ser mayores que 0.")
            .LessThanOrEqualTo(168).WithMessage("Las horas semanales no pueden superar 168.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("El tipo de contrato no es válido.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("El estado del contrato no es válido.");

        RuleFor(x => x.WorkDayType)
            .IsInEnum().WithMessage("El tipo de jornada no es válido.");
    }
}