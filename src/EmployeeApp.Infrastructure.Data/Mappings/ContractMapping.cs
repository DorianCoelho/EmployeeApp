using EmployeeApp.Domain.Core.Entities.Contracts;
using FluentNHibernate.Mapping;

namespace EmployeeApp.Infrastructure.Data.Mappings;

public class ContractMapping : ClassMap<Contract>
{
    public ContractMapping()
    {
        Table("Contracts");
        Id(x => x.Id).GeneratedBy.Identity();

        Map(x => x.StartDate).Not.Nullable();
        Map(x => x.EndDate).Nullable();
        Map(x => x.JobTitle).Not.Nullable();
        Map(x => x.Salary).Precision(18).Scale(2);
        Map(x => x.WeeklyHours).Not.Nullable();


        Map(x => x.Type).CustomType<ContractType>();
        Map(x => x.Status).CustomType<ContractStatus>();


        References(x => x.Employee)
            .Column("EmployeeId")
            .Not.Nullable();


        HasMany(x => x.History)
            .Cascade.AllDeleteOrphan()
            .Inverse()
            .KeyColumn("ContractId");
    }
}