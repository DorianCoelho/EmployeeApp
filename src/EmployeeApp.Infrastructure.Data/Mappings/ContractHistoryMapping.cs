using FluentNHibernate.Mapping;
using EmployeeApp.Domain.Core.Entities.ContractHistories;

namespace EmployeeApp.Infrastructure.Data.Mappings;

public class ContractHistoryMapping : ClassMap<ContractHistory>
{
    public ContractHistoryMapping()
    {
        Table("ContractHistories");

        Id(x => x.Id).GeneratedBy.Identity();

        Map(x => x.Salary).Precision(18).Scale(2).Not.Nullable();
        Map(x => x.JobTitle).Not.Nullable().Length(100);
        Map(x => x.ChangeDate).Not.Nullable();
        Map(x => x.Reason).Nullable().Length(500);
        
        // Mapeo del Enum
        Map(x => x.Type).CustomType<EmployeeApp.Domain.Core.Entities.Contracts.ContractType>();

        // Relación con el Contrato
        // Not.Nullable() es clave porque un historial no existe sin un contrato
        References(x => x.Contract)
            .Column("ContractId")
            .Not.Nullable();
            
        // Si tu BaseModel tiene campos de auditoría, mapealos aquí también
        Map(x => x.CreatedAt).Not.Nullable();
        Map(x => x.UpdatedAt).Nullable();
    }
}