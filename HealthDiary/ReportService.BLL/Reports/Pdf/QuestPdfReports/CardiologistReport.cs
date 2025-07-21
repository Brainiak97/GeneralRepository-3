using QuestPDF.Infrastructure;
using ReportService.BLL.Common.Interfaces;
using ReportService.BLL.Data.Dtos;

namespace ReportService.BLL.Reports.Pdf.QuestPdfReports;

/// <summary>
/// Отчёт приёма кардиолога.
/// </summary>
internal class CardiologistReport : IReport
{
    private readonly CardiologistReportData _reportData;

    public CardiologistReport(CardiologistReportData reportDataDto)
    {
        ArgumentNullException.ThrowIfNull(reportDataDto);
        _reportData = reportDataDto;
    }

    public void Compose(IDocumentContainer container)
    {
        // TODO Собрать шаблон
    }
}