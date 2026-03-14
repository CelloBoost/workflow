using System.Net;
using System.Net.Http.Json;

namespace Workflow.Tests.Integration;

[TestFixture]
public sealed class HealthRestfulTests
{
    [Test]
    public async Task GetHealth_ShouldReturnOkStatus()
    {
        using var requestCts = new CancellationTokenSource(IntegrationFixture.RequestTimeout);
        var response = await IntegrationFixture.Client.GetAsync(
            "/api/workflow/v1/health",
            requestCts.Token
        );

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var body = await response.Content.ReadFromJsonAsync<HealthResponse>();
        Assert.That(body, Is.Not.Null);
        Assert.That(body!.Status, Is.EqualTo("ok"));
    }

    private sealed class HealthResponse
    {
        public string Status { get; set; } = string.Empty;
    }
}
