using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.ExceptionHandlers
{
    /// <summary>
    /// Базовый обработчик исключений
    /// </summary>
    public class BaseExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// Асинхронный обработчик исключений в рамках ASP.NET Core
        /// </summary>
        /// <param name="httpContext">контекст</param>
        /// <param name="exception">Необработанное исключение</param>
        /// <param name="cancellationToken">токен отмены обработки</param>
        /// <returns></returns>
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
