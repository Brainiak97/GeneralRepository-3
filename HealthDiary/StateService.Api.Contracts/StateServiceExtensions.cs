using Microsoft.Extensions.DependencyInjection;
using Refit;
using StateService.Api.Contracts;

namespace MetricService.Api.Contracts
{
    public static class StateServiceExtensions
    {
        /// <summary>
		/// Добавляет http-клиент для обращение к MetricService
		/// </summary>
		/// <param name="services"></param>
		/// <param name="baseAddress">Базовый URL</param>
		/// <returns></returns>
		public static IServiceCollection AddStateServiceClient(this IServiceCollection services, string baseAddress)
        {
            UriBuilder builder = new UriBuilder(baseAddress)
            {
                Path = "/api",
            };
            var uri = builder.Uri;

            services.AddRefitClient<IStateServiceClient>().ConfigureHttpClient(x => x.BaseAddress = uri)
                .AddHttpMessageHandler(s =>
                {
                    return s.GetService<DelegatingHandler>()
                        ?? throw new ArgumentNullException($"Зависимость {typeof(DelegatingHandler)} не найдена");
                });
            ;
            return services;
        }
    }
}
