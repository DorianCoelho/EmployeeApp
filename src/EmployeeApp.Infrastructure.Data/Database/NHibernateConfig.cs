using EmployeeApp.Infrastructure.Data.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace EmployeeApp.Infrastructure.Data.Database;

public static class NHibernateConfig
{
    /// <summary>
    /// Crea la SessionFactory y, según opción, actualiza o recrea el esquema desde los mappings (entidades).
    /// </summary>
    /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
    /// <param name="recreateSchema">Si es true, borra y recrea tablas (solo para desarrollo). Si es false, crea/actualiza sin borrar datos.</param>
    public static ISessionFactory CreateSessionFactory(string connectionString, bool recreateSchema = false)
    {
        return Fluently.Configure()
            .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EmployeeMapping>())
            .ExposeConfiguration(cfg =>
            {
                if (recreateSchema)
                {
                    new SchemaExport(cfg).Execute(false, true, false);
                }
                else
                {
                    new SchemaUpdate(cfg).Execute(false, true);
                }
            })
            .BuildSessionFactory();
    }
}