using ReportService.Api.Contracts.Data.Responses;
using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Common.Generators.Containers;
using ReportService.BLL.Interfaces;
using ReportService.DAL.Interfaces.Providers;
using Shared.Common.Extensions;

namespace ReportService.BLL.Services;

/// <inheritdoc />
internal class ReportService(
    IReportGeneratorsContainer reportGeneratorsContainer,
    IPolyclinicsDataProvider polyclinicsDataProvider)
    : IReportService
{
    /// <inheritdoc />
    public async Task<GenerateReportResponse> GenerateReportAsync(int appointmentResultId, ReportFormat reportFormat)
    {
        var appointmentResult = await polyclinicsDataProvider.GetAppointmentResultById(appointmentResultId);
        if (appointmentResult is null)
        {
            throw new InvalidOperationException(
                $"Ошибка получения результата приёма у врача по идентификатору {appointmentResultId}");
        }

        var reportGenerator = reportGeneratorsContainer.GetGenerator(reportFormat);
        var report = await reportGenerator.GenerateAsync(appointmentResult.ReportTemplateId, appointmentResult.ReportContent);

        return new GenerateReportResponse(
            report,
            $"report_{appointmentResultId}_{DateTime.Now:yyyyMMdd_HHmmss}.{reportFormat.GetDisplayName()}");
    }
}