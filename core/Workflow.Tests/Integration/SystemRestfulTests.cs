using System.Net;
using System.Net.Http.Json;

namespace Workflow.Tests.Integration;

[TestFixture]
public sealed class SystemRestfulTests
{
    [Test]
    public async Task GetSystem_ShouldReturnVersionFromConfiguredEnvironment()
    {
        using var requestCts = new CancellationTokenSource(IntegrationFixture.RequestTimeout);
        var response = await IntegrationFixture.Client.GetAsync(
            "/api/workflow/v1/system",
            requestCts.Token
        );

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var body = await response.Content.ReadFromJsonAsync<SystemResponse>();
        Assert.That(body, Is.Not.Null);
        Assert.That(body!.Version, Is.EqualTo(IntegrationFixture.ExpectedVersion));
    }

    private sealed class SystemResponse
    {
        public string Version { get; set; } = string.Empty;
    }
}
