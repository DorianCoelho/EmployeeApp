using FluentNHibernate.Mapping;
using EmployeeApp.Domain.Core.Entities.Employees;

namespace EmployeeApp.Infrastructure.Data.Mappings;

public class EmployeeMapping : ClassMap<Employee>
{
    public EmployeeMapping()
    {
        Table("Employees");


        Id(x => x.Id).GeneratedBy.Identity();


        Map(x => x.FirstName).Not.Nullable().Length(100);
        Map(x => x.LastName).Not.Nullable().Length(100);
        Map(x => x.Email).Not.Nullable().Unique().Length(150);
        Map(x => x.PhoneNumber).Not.Nullable().Length(20);
        Map(x => x.Address).Not.Nullable().Length(200);
        Map(x => x.City).Not.Nullable().Length(100);


        Map(x => x.CassNumber).Not.Nullable().Unique().Length(20);


        Map(x => x.CreatedAt).Not.Nullable();
        Map(x => x.UpdatedAt).Nullable();


        HasMany(x => x.Contracts)
            .Cascade.AllDeleteOrphan()
            .Inverse()
            .KeyColumn("EmployeeId")
            .LazyLoad();
    }
}