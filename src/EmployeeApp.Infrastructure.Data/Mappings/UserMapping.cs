using FluentNHibernate.Mapping;

namespace EmployeeApp.Infrastructure.Data.Mappings;

public class UserMapping : ClassMap<User>
{
    public UserMapping()
    {
        Table("Users");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Email).Not.Nullable().Unique().Length(150);
        Map(x => x.PasswordHash).Not.Nullable().Length(500);
        Map(x => x.IsActive).Not.Nullable();
        Map(x => x.CreatedAt).Not.Nullable();
    }
}