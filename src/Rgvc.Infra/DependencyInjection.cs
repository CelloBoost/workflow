using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rgvc.Domain.Abstractions;
using Rgvc.Infra.Data;
using Rgvc.Infra.HostedServices;
using Rgvc.Infra.Repositories;

namespace Rgvc.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<RgvcDbContext>(
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
