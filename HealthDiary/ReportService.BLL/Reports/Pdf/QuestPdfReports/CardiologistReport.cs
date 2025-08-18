using QuestPDF.Infrastructure;
using ReportService.Api.Contracts.Data;
using ReportService.BLL.Common.Interfaces;

namespace ReportService.BLL.Reports.Pdf.QuestPdfReports;

/// <summary>
/// Отчёт приёма кардиолога.
/// </summary>
internal class CardiologistReport : IReport
{
    private readonly CardiologistReportDataDto _reportData;

    public CardiologistReport(CardiologistReportDataDto reportDataDto)
    {
        ArgumentNullException.ThrowIfNull(reportDataDto);
        _reportData = reportDataDto;
    }

    public void Compose(IDocumentContainer container)
    {
        // TODO Собрать шаблон
    }
}