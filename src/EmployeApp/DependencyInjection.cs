using EmployeeApp.Services.Contracts.Contracts.Request;
using EmployeeApp.Services.Contracts.Contracts.Validations;
using EmployeeApp.Services.Contracts.Employees.Request;
using EmployeeApp.Services.Contracts.Employees.Validations;
using EmployeeApp.Services.Contracts.Users.Request;
using EmployeeApp.Services.Contracts.Users.Validations;
using FluentValidation;
using Mapster;
using MapsterMapper;

namespace EmployeApp;

public static class DependencyInjection
{
    public static void AddFluentValidators(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services
            .AddTransient<IValidator<GetEmployeeRequest>, GetEmployeeRequestValidator>()
            .AddTransient<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>()
            .AddTransient<IValidator<CreateContractRequest>, CreateContractRequestValidator>()
            .AddTransient<IValidator<EditContractRequest>, EditContractRequestValidator>()
            .AddTransient<IValidator<AuthRequest>, AuthRequestValidator>();
        services.AddScoped<IMapper, ServiceMapper>();
    }

    public static void AddMappings(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}