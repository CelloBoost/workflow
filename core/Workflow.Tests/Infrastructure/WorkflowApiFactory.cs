using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Workflow.Tests.Infrastructure;

public sealed class WorkflowApiFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;
    private readonly string _systemVersion;

    public WorkflowApiFactory(string connectionString, string systemVersion)
    {
        _connectionString = connectionString;
        _systemVersion = systemVersion;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureAppConfiguration(
            (_, configurationBuilder) =>
            {
                configurationBuilder.AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["ConnectionStrings:DefaultConnection"] = _connectionString,
                        ["System:Version"] = _systemVersion,
                    }
                );
            }
        );
    }
}
