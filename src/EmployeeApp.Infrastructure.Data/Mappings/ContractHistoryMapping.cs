using FluentNHibernate.Mapping;
using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Domain.Core.Entities.Contracts;

namespace EmployeeApp.Infrastructure.Data.Mappings;

public class ContractHistoryMapping : ClassMap<ContractHistory>
{
    public ContractHistoryMapping()
    {
        Table("ContractHistories");

        Id(x => x.Id).GeneratedBy.Identity();

        Map(x => x.Salary).Precision(18).Scale(2).Not.Nullable();
        Map(x => x.JobTitle).Not.Nullable().Length(150);

        Map(x => x.WeeklyHours).Not.Nullable();

        Map(x => x.ChangeDate).Not.Nullable();
        Map(x => x.Reason).Nullable().Length(500);

        // Enums
        Map(x => x.Type).CustomType<ContractType>().Not.Nullable();
        Map(x => x.Status).CustomType<ContractStatus>().Not.Nullable();
        Map(x => x.WorkDayType).CustomType<WorkDayType>().Not.Nullable();


        References(x => x.Contract)
            .Column("ContractId")
            .Not.Nullable();

        Map(x => x.CreatedAt).Not.Nullable();
        Map(x => x.UpdatedAt).Nullable();

        // Si mantienes ContractId como propiedad en la entidad, puedes mapearla read-only:
        // Map(x => x.ContractId)
        //    .Column("ContractId")
        //    .Insert(false)
        //    .Update(false);
    }
}