using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Web;

public static class CorrelationIdExtensions
{
    private const string CorrelationIdHeaderName = "X-Correlation-ID";

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app.Use(async (ctx, next) =>
        {
            if (!ctx.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationId))
                correlationId = Guid.NewGuid().ToString("N");

            ctx.Items[CorrelationIdHeaderName] = correlationId.ToString();
            await next();
        });
    }

    public static Guid GetCorrelationId(this HttpContext context)
    {
        context.Items.TryGetValue(CorrelationIdHeaderName, out var correlationId);
        return string.IsNullOrEmpty(correlationId?.ToString()) ? Guid.NewGuid() : new Guid(correlationId.ToString()!);
    }
}