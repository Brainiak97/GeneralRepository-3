using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.ExceptionHandlers
{
    public class BaseExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = 400;
            httpContext.Response.ContentType = "application/json";
            var problem = new ProblemDetails()
            {
                Status = httpContext.Response.StatusCode,
                Title = "Internal Server Error",
                Detail = exception.Message,
                Instance = httpContext.Request.Path,
                Type = "ServerError",
                Extensions = new Dictionary<string, object?>()
                {
                   {"Request_param", exception.Data}
                }
            };
            
            problem.Extensions["traceId"] = httpContext.TraceIdentifier;
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true;
        }
    }
}
