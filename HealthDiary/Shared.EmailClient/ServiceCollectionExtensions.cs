using Microsoft.Extensions.DependencyInjection;

namespace Shared.EmailClient
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailServiceClient(this IServiceCollection services, string emailServiceUrl)
        {
            services.AddHttpClient<IEmailServiceClient, HttpEmailServiceClient>(client =>
            {
                client.BaseAddress = new Uri(emailServiceUrl);
            });

            return services;
        }
    }
}
