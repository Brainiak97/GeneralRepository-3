namespace FoodService.Api.Middlewares
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
