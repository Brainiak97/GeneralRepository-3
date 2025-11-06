using AutoMapper;
using MassTransit;
using ReportService.Api.Contracts.Events;
using ReportService.BLL.Data.Commands;
using ReportService.BLL.Interfaces;
using ReportService.Domain.Models;

namespace ReportService.BLL.Consumers;

/// <summary>
/// Обработчик сообщения о запросе генерации отчёта. 
/// </summary>
/// <param name="reportService"><see cref="IReportService"/>.</param>
/// <param name="emailSendService"><see cref="IEmailSendService"/>.</param>
/// <param name="mapper"><see cref="IMapper"/>.</param>
public class GenerateReportRequestedConsumer(
    IReportService reportService,
    IEmailSendService emailSendService,
    IMapper mapper)
    : IConsumer<GenerateReportRequested>
{
    /// <inheritdoc />
    public async Task Consume(ConsumeContext<GenerateReportRequested> context)
    {
        var message = context?.Message ?? throw new ArgumentNullException(nameof(context));

        var reportFormat = mapper.Map<ReportFormat>(message.ReportFormat);
        var generatedReport = await reportService.GenerateReportAsync(
            new GenerateReportCommand(
                message.EntityId,
                message.ReportContent,
                message.ReportTemplateId,
                reportFormat));

        var reportId = await reportService.AddReportAsync(
            new AddReportCommand
            {
                ReportId = message.EntityId,
                ReportFormat = reportFormat,
                FileName = generatedReport.FileName,
                Content = generatedReport.Content,
            });

        if (message.NeedSendToEmail)
        {
            await emailSendService.SendReportAsync(
                reportId,
                message.EmailAddress!,
                generatedReport.Content,
                generatedReport.FileName,
                reportFormat);
        }
    }
}