using EmployeeApp.Domain.Core.Entities;

public class User : BaseModel
{
    public virtual int Id { get; set; }
    public virtual string Email { get; set; } = null!;
    public virtual string PasswordHash { get; set; } = null!;
    public virtual bool IsActive { get; set; } = true;
}