namespace EmployeeApp.Domain.Core.Entities;

public class BaseModel
{
    public virtual DateTime CreatedAt { get; set; }

    public virtual DateTime UpdatedAt { get; set; }
}