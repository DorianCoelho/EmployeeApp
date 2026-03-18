using System.ComponentModel.DataAnnotations;
using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Services.Contracts.ContractHistories.Request;

public class CreateContractHistoryRequest
{
    [Required(ErrorMessage = "El contrato es obligatorio.")]
    public int ContractId { get; set; }

    [Required(ErrorMessage = "El salario es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El salario debe ser mayor que 0.")]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "El puesto de trabajo es obligatorio.")]
    [StringLength(150, ErrorMessage = "El puesto de trabajo no puede superar los 150 caracteres.")]
    public string JobTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Las horas semanales son obligatorias.")]
    [Range(0.01, 168, ErrorMessage = "Las horas semanales deben estar entre 0 y 168.")]
    public int WeeklyHours { get; set; }

    [Required(ErrorMessage = "El tipo de contrato es obligatorio.")]
    public ContractType Type { get; set; }

    [Required(ErrorMessage = "El estado del contrato es obligatorio.")]
    public ContractStatus Status { get; set; }

    [Required(ErrorMessage = "El tipo de jornada es obligatorio.")]
    public WorkDayType WorkDayType { get; set; }

    // Si lo genera servidor, puedes quitarlo del request.
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

    [StringLength(500, ErrorMessage = "El motivo no puede superar los 500 caracteres.")]
    public string? Reason { get; set; }
}