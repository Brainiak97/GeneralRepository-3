using Microsoft.Extensions.DependencyInjection;

namespace Shared.EmailClient
{
    /// <summary>
    /// Содержит методы расширения для регистрации email-клиента в коллекции сервисов.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет HTTP-клиент для взаимодействия с email-сервисом в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI (внедрения зависимостей).</param>
        /// <param name="emailServiceUrl">Базовый URL email-сервиса, к которому будут отправляться запросы.</param>
        /// <returns>Обновлённая коллекция сервисов.</returns>
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