using FluentValidation;
using EmployeeApp.Services.Contracts.ContractHistories.Request;

namespace EmployeeApp.Services.Contracts.ContractHistories.Validations;

public class CreateContractHistoryRequestValidator : AbstractValidator<CreateContractHistoryRequest>
{
    public CreateContractHistoryRequestValidator()
    {
        RuleFor(x => x.ContractId)
            .GreaterThan(0).WithMessage("El contrato es obligatorio.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("El salario debe ser mayor que 0.");

        RuleFor(x => x.JobTitle)
            .NotEmpty().WithMessage("El puesto de trabajo es obligatorio.")
            .MaximumLength(150).WithMessage("El puesto de trabajo no puede superar los 150 caracteres.");

        RuleFor(x => x.WeeklyHours)
            .GreaterThan(0).WithMessage("Las horas semanales deben ser mayores que 0.")
            .LessThanOrEqualTo(168).WithMessage("Las horas semanales no pueden superar 168.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("El tipo de contrato no es válido.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("El estado del contrato no es válido.");

        RuleFor(x => x.WorkDayType)
            .IsInEnum().WithMessage("El tipo de jornada no es válido.");

        RuleFor(x => x.ChangeDate)
            .NotEmpty().WithMessage("La fecha del cambio es obligatoria.")
            .LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(5))
            .WithMessage("La fecha del cambio no puede ser futura.");

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage("El motivo no puede superar los 500 caracteres.");
    }
}