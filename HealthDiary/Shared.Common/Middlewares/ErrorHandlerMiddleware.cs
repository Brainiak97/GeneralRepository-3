using Microsoft.AspNetCore.Http;
using Shared.Common.Exceptions;

namespace Shared.Common.Middlewares
{
	/// <summary>
	/// Middleware для обработки ошибок API методов
	/// </summary>
	public class ErrorHandlerMiddleware
	{
		/// <summary>
		/// Следующий middleware
		/// </summary>
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware( RequestDelegate next )
		{
			_next = next;
		}

		public async Task InvokeAsync( HttpContext context )
		{
			try
			{
				await _next( context );
			}
			catch ( EntryNotFoundException exc )
			{
				//TODO log
				Console.WriteLine( $"Возникла ошибка: {exc}" );
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync( new { Message = exc.Message } );
			}
			catch ( Exception exc )
			{
				//TODO log
				//await context.Response.WriteAsync( $"Возникла ошибка: {exc.Message}" );
				Console.WriteLine( $"Возникла ошибка: {exc}" );
				throw;
			}
		}
	}
}
