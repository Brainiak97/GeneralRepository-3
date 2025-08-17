using Microsoft.Extensions.DependencyInjection;
using ReportService.BLL.Common.Generators.Pdf;
using ReportService.BLL.Common.ReportFactory;
using ReportService.BLL.Interfaces;

namespace ReportService.BLL.Infrastructure;

/// <summary>
/// Расширения для регистрации сервисов слоя работы с отчётами.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы слоя работы с отчётами.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services
            .AddCommon()
            .AddServices();

    private static IServiceCollection AddCommon(this IServiceCollection services) =>
        services
            .AddSingleton<IPdfReportGenerator, QuestPdfReportGenerator>()
            .AddSingleton<IReportGeneratorFactory, ReportGeneratorFactory>();

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddScoped<IReportService, Services.ReportService>();
}