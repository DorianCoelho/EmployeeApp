using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Services.ContractHistories;
using EmployeeApp.Services.Contracts.ContractHistories.Dto;
using EmployeeApp.Services.Contracts.Contracts.Dto;
using EmployeeApp.Services.Contracts.Employees.Dto;

namespace EmployeeApp.Services.ContractHistories
{
    public partial class ContractHistoryMapper : IContractHistoryMapper
    {
        public ContractHistoryDto MapTo(ContractHistory p1)
        {
            return p1 == null ? null : new ContractHistoryDto()
            {
                Id = p1.Id,
                ContractId = p1.ContractId,
                Contract = p1.Contract == null ? null : new ContractDto()
                {
                    Id = p1.Contract.Id,
                    EmployeeId = p1.Contract.EmployeeId,
                    Employee = p1.Contract.Employee == null ? null : new EmployeeDto()
                    {
                        Id = p1.Contract.Employee.Id,
                        FirstName = p1.Contract.Employee.FirstName,
                        LastName = p1.Contract.Employee.LastName,
                        Email = p1.Contract.Employee.Email,
                        PhoneNumber = p1.Contract.Employee.PhoneNumber,
                        Address = p1.Contract.Employee.Address,
                        City = p1.Contract.Employee.City,
                        CassNumber = p1.Contract.Employee.CassNumber
                    },
                    StartDate = p1.Contract.StartDate,
                    EndDate = p1.Contract.EndDate,
                    JobTitle = p1.Contract.JobTitle,
                    Salary = p1.Contract.Salary,
                    WeeklyHours = p1.Contract.WeeklyHours,
                    Type = p1.Contract.Type,
                    Status = p1.Contract.Status,
                    WorkDayType = p1.Contract.WorkDayType
                },
                Salary = p1.Salary,
                JobTitle = p1.JobTitle,
                WeeklyHours = p1.WeeklyHours,
                Type = p1.Type,
                Status = p1.Status,
                WorkDayType = p1.WorkDayType,
                ChangeDate = p1.ChangeDate,
                Reason = p1.Reason
            };
        }
    }
}