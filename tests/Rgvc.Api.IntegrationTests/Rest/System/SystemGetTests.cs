using System.Net;
using System.Net.Http.Json;
using Rgvc.Api.IntegrationTests.Infrastructure;
using Testcontainers.PostgreSql;

namespace Rgvc.Api.IntegrationTests.Rest.System;

[TestFixture]
public sealed class SystemGetTests
{
    private const string ExpectedVersion = "integration-test-version";
    private static readonly TimeSpan StartupTimeout = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(30);

    private PostgreSqlContainer _postgresContainer = null!;
    private RgvcApiFactory _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Environment.SetEnvironmentVariable("TESTCONTAINERS_RYUK_DISABLED", "true");
        _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:17")
            .WithDatabase("rgvc_integration")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        using (var startupCts = new CancellationTokenSource(StartupTimeout))
        {
            await _postgresContainer.StartAsync(startupCts.Token);
        }

        _factory = new RgvcApiFactory(_postgresContainer.GetConnectionString(), ExpectedVersion);
        _client = _factory.CreateClient();
        _client.Timeout = RequestTimeout;
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        _client.Dispose();

        if (_factory is not null)
        {
            await _factory.DisposeAsync();
        }

        if (_postgresContainer is not null)
        {
            await _postgresContainer.DisposeAsync();
        }
    }

    [Test]
    public async Task GetSystem_ShouldReturnVersionFromConfiguredEnvironment()
    {
        using var requestCts = new CancellationTokenSource(RequestTimeout);
        var response = await _client.GetAsync("/api/rgvc/v1/system", requestCts.Token);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var body = await response.Content.ReadFromJsonAsync<SystemResponse>();
        Assert.That(body, Is.Not.Null);
        Assert.That(body!.Version, Is.EqualTo(ExpectedVersion));
    }

    private sealed class SystemResponse
    {
        public string Version { get; set; } = string.Empty;
    }
}
