using MetricService.BLL.Exceptions;
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
            var (statusCode, problemDetails) = GetProblemDetailsAndStatusCode(exception);

            httpContext.Response.StatusCode = statusCode;

            problemDetails.Instance = httpContext.Request.Path;

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private (int, ProblemDetails) GetProblemDetailsAndStatusCode(Exception exception)
        {
            switch (exception)
            {
                case ViolationAccessException:
                case ValidateModelException:
                    {
                        return (
                            StatusCodes.Status400BadRequest,
                             new ProblemDetails
                             {
                                 Status = StatusCodes.Status400BadRequest,
                                 Title = "Bad request",
                                 Detail = exception.Message,
                                 Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                                 Extensions = new Dictionary<string, object?>()
                                        {
                                           {"Request_param", exception.Data}
                                        }
                             });
                    }
                case IncorrectOrEmptyResultException:
                    {
                        return (
                            StatusCodes.Status404NotFound,
                            new ProblemDetails
                            {
                                Status = StatusCodes.Status404NotFound,
                                Title = "Resource not found",
                                Detail = exception.Message,
                                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                                Extensions = new Dictionary<string, object?>()
                                    {
                                       {"Request_param", exception.Data}
                                    }
                            });
                    }
                default:
                    {
                        return (
                            StatusCodes.Status500InternalServerError,
                            new ProblemDetails
                            {
                                Status = StatusCodes.Status500InternalServerError,
                                Title = "Server error",
                                Detail = exception.Message,
                                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                                Extensions = new Dictionary<string, object?>()
                                    {
                                       {"Request_param", exception.Data}
                                    }
                            });
                    }
            }
        }
    }
}
