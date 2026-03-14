using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Workflow.Domain.Models;
using Workflow.Infra.Data;

namespace Workflow.Infra.HostedServices;

public class SystemDataInitializer : IHostedService
{
    private const string DefaultVersionFallback = "fallback-version";

    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SystemDataInitializer> _logger;

    public SystemDataInitializer(
        IConfiguration configuration,
        IServiceScopeFactory scopeFactory,
        ILogger<SystemDataInitializer> logger
    )
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WorkflowDbContext>();

        await dbContext.Database.MigrateAsync(cancellationToken);

        var configuredVersion = GetConfiguredVersion();

        var systemData = await dbContext.SystemData.SingleOrDefaultAsync(cancellationToken);
        if (systemData is null)
        {
            await InitializeSystemDataAsync(
                dbContext,
                configuredVersion ?? DefaultVersionFallback,
                cancellationToken
            );
            return;
        }

        if (
            configuredVersion is null
            || string.Equals(systemData.Version, configuredVersion, StringComparison.Ordinal)
        )
        {
            return;
        }

        await OverwriteSystemVersionAsync(
            dbContext,
            systemData,
            configuredVersion,
            cancellationToken
        );
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private string? GetConfiguredVersion()
    {
        var value = _configuration["System:Version"]?.Trim();
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }

    private async Task InitializeSystemDataAsync(
        WorkflowDbContext dbContext,
        string version,
        CancellationToken cancellationToken
    )
    {
        dbContext.SystemData.Add(new SystemData { Id = 1, Version = version });
        await dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Initialized system data with version {Version}.", version);
    }

    private async Task OverwriteSystemVersionAsync(
        WorkflowDbContext dbContext,
        SystemData systemData,
        string version,
        CancellationToken cancellationToken
    )
    {
        systemData.Version = version;
        await dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation(
            "Overwrote system version from environment. version={Version}",
            version
        );
    }
}
