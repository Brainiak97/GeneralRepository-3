using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace UserService.Api.Contracts
{
    public static class UserServiceExtensions
    {
        /// <summary>
        /// Добавляет http-клиент для обращение к UserService
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseAddress">Базовый URL</param>
        /// <returns></returns>
        public static IServiceCollection AddUserServiceClient( this IServiceCollection services, string baseAddress )
        {
            UriBuilder builder = new UriBuilder( baseAddress )
            {
                Path = "api",
            };
            var uri = builder.Uri;

            services.AddRefitClient<IUserServiceClient>().ConfigureHttpClient( x => x.BaseAddress = uri );
            return services;
        }
    }
}
