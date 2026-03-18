using EmployeeApp.Infrastructure.Data;
using EmployeeApp.Services.ContractHistories;
using EmployeeApp.Services.Contracts;
using EmployeeApp.Services.Contracts.ContractHistories;
using EmployeeApp.Services.Contracts.Contracts;
using EmployeeApp.Services.Contracts.Employees;
using EmployeeApp.Services.Contracts.Users;
using EmployeeApp.Services.Employees;
using EmployeeApp.Services.Users;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeApp.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureData(configuration);

        services.AddScoped<IUnitOfService, UnitOfService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IContractHistoryService, ContractHistoryService>();

        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);


        return services;
    }
}