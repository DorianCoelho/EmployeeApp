using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Infrastructure.Contracts.Users;
using EmployeeApp.Infrastructure.Data.Areas.ContractHistories;
using EmployeeApp.Infrastructure.Data.Areas.Contracts;
using EmployeeApp.Infrastructure.Data.Areas.Employees;
using EmployeeApp.Infrastructure.Data.Areas.Users;
using EmployeeApp.Infrastructure.Data.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace EmployeeApp.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var recreateSchema = bool.TryParse(configuration["NHibernate:RecreateSchema"], out var recreate) && recreate;

        
        services.AddSingleton(sp => NHibernateConfig.CreateSessionFactory(connectionString!, recreateSchema));
        services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());

        services.AddScoped<IContractUnitOfWork, ContractUnitOfWork>();

        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContractHistoryRepository, ContractHistoryRepository>();
        
        


        return services;
    }
}