using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;
using EmployeeApp.Services.Contracts.Employees.Request;
using EmployeeApp.Services.Employees;

namespace EmployeeApp.Services.Employees
{
    public partial class EmployeeMapper : IEmployeeMapper
    {
        public EmployeeDto MapToDto(Employee p1)
        {
            return p1 == null ? null : new EmployeeDto()
            {
                Id = p1.Id,
                FirstName = p1.FirstName,
                LastName = p1.LastName,
                Email = p1.Email,
                PhoneNumber = p1.PhoneNumber,
                Address = p1.Address,
                City = p1.City,
                CassNumber = p1.CassNumber
            };
        }
        public CreateEmployeeRequest MapToCreateRequestDto(CreateEmployeeFilter p2)
        {
            return p2 == null ? null : new CreateEmployeeRequest()
            {
                FirstName = p2.FirstName,
                LastName = p2.LastName,
                Email = p2.Email,
                PhoneNumber = p2.PhoneNumber,
                Address = p2.Address,
                City = p2.City,
                CassNumber = p2.CassNumber
            };
        }
    }
}