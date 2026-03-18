using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeApp.Services.Contracts.Employees.Request;

public class CreateEmployeeRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [Display(Name = "Nombre")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [Display(Name = "Apellido")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El Teléfono es obligatorio")]
    [Display(Name = "Teléfono")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "La Dirección es obligatorio")]
    [Display(Name = "Dirección")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "La Ciudad es obligatorio")]
    [Display(Name = "Ciudad")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "El número CASS es obligatorio")]
    [Display(Name = "Nº CASS")]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "El Nº CASS debe tener exactamente 7 caracteres.")]
    [RegularExpression(@"^\d{6}[A-Z]$",
        ErrorMessage = "El Nº CASS debe ser 6 números y 1 letra mayúscula (ej: 000000A).")]
    public string CassNumber { get; set; } = null!;
}