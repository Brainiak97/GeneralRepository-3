using QuestPDF.Infrastructure;

namespace ReportService.BLL.Reports.Pdf.QuestPdfReports.Interfaces;

/// <summary>
/// Обобщенный интерфейс шаблона отчёта.
/// </summary>
public interface IReportTemplate
{
    /// <summary>
    /// Собрать отчёт.
    /// </summary>
    /// <param name="container"><see cref="IDocumentContainer"/>.</param>
    /// <param name="data">Данные отчёта (json).</param>
    public void Compile(IDocumentContainer container, string data);
}