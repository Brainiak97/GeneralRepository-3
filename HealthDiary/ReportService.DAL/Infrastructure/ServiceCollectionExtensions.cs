using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportService.DAL.Contexts;
using ReportService.DAL.Interfaces.Repositories;
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
            .AddDbContext<ReportServiceDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("DbConnectionString"));
                },
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Singleton)
            .AddRepositories();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IReportsRepository, ReportsRepository>();
}