using Workflow.Tests.Infrastructure;
using Testcontainers.PostgreSql;

namespace Workflow.Tests.Integration;

[SetUpFixture]
public sealed class IntegrationFixture
{
    private static readonly TimeSpan StartupTimeout = TimeSpan.FromSeconds(60);

    public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(30);
    public const string ExpectedVersion = "integration-test-version";

    private PostgreSqlContainer? _postgresContainer;
    private WorkflowApiFactory? _factory;
    private static HttpClient? _client;

    public static HttpClient Client =>
        _client
        ?? throw new InvalidOperationException("Integration test client is not initialized.");

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Environment.SetEnvironmentVariable("TESTCONTAINERS_RYUK_DISABLED", "true");
        _postgresContainer = new PostgreSqlBuilder("postgres:17")
            .WithDatabase("workflow_integration")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        using var startupCts = new CancellationTokenSource(StartupTimeout);
        await _postgresContainer.StartAsync(startupCts.Token);

        _factory = new WorkflowApiFactory(_postgresContainer.GetConnectionString(), ExpectedVersion);
        _client = _factory.CreateClient();
        _client.Timeout = RequestTimeout;
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        _client?.Dispose();

        if (_factory is not null)
        {
            await _factory.DisposeAsync();
        }

        if (_postgresContainer is not null)
        {
            await _postgresContainer.DisposeAsync();
        }
    }
}
