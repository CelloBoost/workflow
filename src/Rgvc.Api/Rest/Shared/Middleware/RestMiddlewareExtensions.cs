namespace Rgvc.Api.Rest.Shared.Middleware;

public static class RestMiddlewareExtensions
{
    public static IApplicationBuilder UseRestMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestTimingMiddleware>();
    }
}
