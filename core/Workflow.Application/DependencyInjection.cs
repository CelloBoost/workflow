using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Abstractions;
using Workflow.Application.Services;

namespace Workflow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISystemService, SystemService>();
        return services;
    }
}
