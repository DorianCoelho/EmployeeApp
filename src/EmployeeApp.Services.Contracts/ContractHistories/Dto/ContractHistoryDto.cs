using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Services.Contracts.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Dto;

namespace EmployeeApp.Services.Contracts.ContractHistories.Dto;

public class ContractHistoryDto
{
    public int Id { get; set; }

    public int ContractId { get; set; }
    
    public virtual ContractDto Contract { get; set; } = null!; 

    public decimal Salary { get; set; }
    
    public string JobTitle { get; set; } = null!;
    
    public ContractType Type { get; set; }

    public DateTime ChangeDate { get; set; } = DateTime.Now;
    
    public string? Reason { get; set; }
}