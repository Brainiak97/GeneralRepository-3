using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportService.DAL.Common.InMemoryStorages;
using ReportService.DAL.Common.InMemoryStorages.Interfaces;
using ReportService.DAL.Interfaces.Providers;
using ReportService.DAL.Interfaces.Repositories;
using ReportService.DAL.Providers;
using ReportService.DAL.Repositories;

namespace ReportService.DAL.Infrastructure;

/// <summary>
/// Расширения для регистрации сервисов слоя взаимодействия с данными.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы слоя взаимодействия с данными микросервиса.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"><see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddInMemoryStorages()
            .AddRepositories()
            .AddHttpDataProviders();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddSingleton<IReportTemplatesRepository, ReportTemplatesRepository>();

    private static IServiceCollection AddInMemoryStorages(this IServiceCollection services) =>
        services.AddSingleton<IReportTemplatesInMemoryStorage, ReportTemplatesInMemoryStorage>();

    private static IServiceCollection AddHttpDataProviders(this IServiceCollection services)
    {
        services.AddHttpClient<IPolyclinicsDataProvider, PolyclinicsDataProvider>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:7771");
        });

        return services;
    }
}