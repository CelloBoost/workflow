using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Domain.Abstractions;
using Workflow.Infra.Data;
using Workflow.Infra.HostedServices;
using Workflow.Infra.Repositories;

namespace Workflow.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<WorkflowDbContext>(
            (serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException(
                        "ConnectionStrings:DefaultConnection is required."
                    );
                }

                options.UseNpgsql(connectionString);
            }
        );

        services.AddScoped<ISystemRepository, SystemRepository>();
        services.AddHostedService<SystemDataInitializer>();

        return services;
    }
}
