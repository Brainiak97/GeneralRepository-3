using ReportService.BLL.Reports.Pdf.QuestPdfReports.Interfaces;

namespace ReportService.BLL.Common.Templates.QuestPdf.Containers;

/// <summary>
/// Контейнер с шаблонами отчётов QuestPdf.
/// </summary>
public interface IReportTemplatesContainer
{
    /// <summary>
    /// Получить шаблон отчёта.
    /// </summary>
    /// <param name="templateName">Имя шаблона.</param>
    /// <returns></returns>
    IReportTemplate GetReportTemplate(string templateName);
}