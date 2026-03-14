using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}
