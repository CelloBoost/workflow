using System.Diagnostics;

namespace Workflow.Api.Rest.Shared.Middleware;

public class RequestTimingMiddleware
{
    private const long SlowRequestThresholdMs = 1000;

    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds <= SlowRequestThresholdMs)
        {
            return;
        }

        _logger.LogWarning(
            "Slow request. method={Method} path={Path} status={StatusCode} elapsedMs={ElapsedMs}",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds
        );
    }
}
