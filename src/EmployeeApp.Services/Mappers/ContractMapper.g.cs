using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Services.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Dto;

namespace EmployeeApp.Services.Contracts
{
    public partial class ContractMapper : IContractMapper
    {
        public ContractDto MapTo(ContractHistory p1)
        {
            return p1 == null ? null : new ContractDto()
            {
                Id = p1.Id,
                JobTitle = p1.JobTitle,
                Salary = p1.Salary,
                WeeklyHours = p1.WeeklyHours,
                Type = p1.Type,
                Status = p1.Status,
                WorkDayType = p1.WorkDayType
            };
        }
    }
}