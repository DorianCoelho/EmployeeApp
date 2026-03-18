using System.ComponentModel.DataAnnotations;
using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Services.Contracts.Contracts.Request;

public class EditContractRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El empleado es obligatorio.")]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required(ErrorMessage = "El puesto de trabajo es obligatorio.")]
    [StringLength(150, ErrorMessage = "El puesto de trabajo no puede superar los 150 caracteres.")]
    public string JobTitle { get; set; } = null!;

    [Required(ErrorMessage = "El salario es obligatorio.")]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Las horas semanales son obligatorias.")]
    public int WeeklyHours { get; set; }

    [Required(ErrorMessage = "El tipo de contrato es obligatorio.")]
    public ContractType Type { get; set; }

    [Required(ErrorMessage = "El estado del contrato es obligatorio.")]
    public ContractStatus Status { get; set; }

    [Required(ErrorMessage = "El tipo de jornada es obligatorio.")]
    public WorkDayType WorkDayType { get; set; }

    public string? Reason { get; set; }
}