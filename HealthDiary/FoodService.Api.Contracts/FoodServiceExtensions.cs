using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FoodService.Api.Contracts
{
	public static class FoodServiceExtensions
	{
		/// <summary>
		/// Добавляет http-клиент для обращение к FoodService
		/// </summary>
		/// <param name="services"></param>
		/// <param name="baseAddress">Базовый URL</param>
		/// <returns></returns>
		public static IServiceCollection AddFoodServiceClient( this IServiceCollection services, string baseAddress )
		{
			UriBuilder builder = new UriBuilder( baseAddress )
			{
				Path = "food",
			};
			var uri = builder.Uri;

			services.AddRefitClient<IFoodServiceClient>().ConfigureHttpClient( x => x.BaseAddress = uri );
			return services;
		}
	}
}
