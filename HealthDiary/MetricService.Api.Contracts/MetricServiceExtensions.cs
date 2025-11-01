using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace MetricService.Api.Contracts
{
    public static class MetricServiceExtensions
    {
        /// <summary>
		/// Добавляет http-клиент для обращение к MetricService
		/// </summary>
		/// <param name="services"></param>
		/// <param name="baseAddress">Базовый URL</param>
		/// <returns></returns>
		public static IServiceCollection AddMetricServiceClient(this IServiceCollection services, string baseAddress)
        {
            UriBuilder builder = new UriBuilder(baseAddress)
            {
                Path = "/api",
            };
            var uri = builder.Uri;

            services.AddRefitClient<IMetricServiceClient>().ConfigureHttpClient(x => x.BaseAddress = uri);
            return services;
        }
    }
}
