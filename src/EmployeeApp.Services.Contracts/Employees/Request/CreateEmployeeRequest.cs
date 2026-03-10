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

    [Display(Name = "Teléfono")]
    public string PhoneNumber { get; set; } = null!;

    [Display(Name = "Dirección")]
    public string Address { get; set; } = null!;

    [Display(Name = "Ciudad")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "El número CASS es obligatorio")]
    [Display(Name = "Nº CASS")]
    public string CassNumber { get; set; } = null!;
}