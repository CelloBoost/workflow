using Microsoft.Extensions.DependencyInjection;
using Rgvc.Application.Abstractions;
using Rgvc.Application.Services;

namespace Rgvc.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISystemService, SystemService>();
        return services;
    }
}
