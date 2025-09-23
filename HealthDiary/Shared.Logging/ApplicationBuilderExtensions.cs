using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace Shared.Logging
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCommonRequestLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.000} ms";

                // Гибкий уровень логирования
                options.GetLevel = (httpContext, elapsed, ex) =>
                {
                    if (ex != null) return LogEventLevel.Error;
                    if (httpContext.Response.StatusCode >= 400) return LogEventLevel.Warning;
                    return LogEventLevel.Information;
                };

                // Обогащение контекста
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value ?? "(unknown-host)");
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("RequestPath", httpContext.Request.Path);

                    var correlationId = httpContext.Response.Headers["X-Correlation-ID"].FirstOrDefault();
                    diagnosticContext.Set("CorrelationId", correlationId ?? "(none)");

                    var userName = httpContext.User?.Identity?.Name;
                    diagnosticContext.Set("UserId", userName ?? "(anonymous)");

                    diagnosticContext.Set("RequestBodySize", httpContext.Request.ContentLength ?? 0);
                };
            });

            return app;
        }
    }
}
