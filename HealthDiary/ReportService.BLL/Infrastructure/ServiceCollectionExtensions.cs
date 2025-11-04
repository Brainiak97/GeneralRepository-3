using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ReportService.BLL.Common.DataSources.Containers;
using ReportService.BLL.Common.Generators.Factories;
using ReportService.BLL.Common.Generators.Pdf;
using ReportService.BLL.Common.Templates.QuestPdf.Containers;
using ReportService.BLL.Data.Commands;
using ReportService.BLL.Interfaces;
using ReportService.BLL.Services;
using ReportService.BLL.Validators;
using Shared.EmailService.Common;

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
            .AddServices()
            .AddValidators();

    private static IServiceCollection AddCommon(this IServiceCollection services) =>
        services
            .AddScoped<IPdfReportGenerator, PdfReportGenerator>()
            .AddScoped<IReportGeneratorFactory>(provider =>
                new ReportGeneratorFactory(provider.GetRequiredService<IPdfReportGenerator>))
            .AddSingleton<IReportTemplatesContainer, ReportTemplatesContainer>()
            .AddSingleton<IDataSourceInstancesContainer, DataSourceInstancesContainer>()
            .AddEmailMessageBuilder();

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<IReportService, Services.ReportService>()
            .AddScoped<IEmailSendService, EmailSendService>();   

    private static IServiceCollection AddValidators(this IServiceCollection services) =>
        services
            .AddSingleton<IValidator<IServiceCommand>, ServiceCommandValidator>()
            .AddSingleton<IValidator<GenerateReportCommand>, GenerateReportCommandValidator>();
}