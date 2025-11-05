using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolyclinicService.DAL.Contexts;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.DAL.Repositories;

namespace PolyclinicService.DAL.Infrastructure;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы слоя взаимодействия с базой данных микросервиса.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"><see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddDbContext<PolyclinicServiceDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("DbConnectionString"));
                },
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Singleton)
            .AddRepositories();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IAppointmentSlotsRepository, AppointmentSlotsRepository>()
            .AddScoped<IDoctorsRepository, DoctorsRepository>()
            .AddScoped<IAppointmentResultsRepository, AppointmentResultsRepository>()
            .AddScoped<IPolyclinicsRepository, PolyclinicsRepository>();
}