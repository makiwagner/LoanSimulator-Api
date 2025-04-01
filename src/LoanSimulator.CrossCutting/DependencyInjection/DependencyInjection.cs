using LoanSimulator.Application.Mappings;
using LoanSimulator.Application.Services;
using LoanSimulator.Domain.Interfaces;
using LoanSimulator.Domain.Services;
using LoanSimulator.Infrastructure.Data;
using LoanSimulator.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanSimulator.CrossCutting.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabaseDependencies(services, configuration);
        AddMappingDependencies(services);
        AddServiceDependencies(services);
        AddInfrastructureDependencies(services);

        return services;
    }

    private static void AddDatabaseDependencies(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddMappingDependencies(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(LoanProfile));
    }

    private static void AddServiceDependencies(IServiceCollection services)
    {
        services.AddScoped<LoanService>();
        services.AddScoped<LoanApplicationService>();
    }

    private static void AddInfrastructureDependencies(IServiceCollection services)
    {
        services.AddScoped<ILoanRepository, LoanRepository>();
    }
}
