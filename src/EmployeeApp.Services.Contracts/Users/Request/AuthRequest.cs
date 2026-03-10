using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Services.Contracts.Users.Request;

public class AuthRequest
{
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;
}