using ReportService.Domain.Models;

namespace ReportService.Common.Helpers;

/// <summary>
/// Класс предоставляющий вспомогательные методы для ReportService.
/// </summary>
public static class ReportServiceHelper
{
    /// <summary>
    /// Вернуть ContentType по типу отчёта.
    /// </summary>
    /// <param name="reportFormat"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetContentTypeByFormat(ReportFormat reportFormat) =>
        reportFormat switch
        {
            ReportFormat.Pdf => "application/pdf",
            _ => throw new ArgumentOutOfRangeException(nameof(reportFormat), reportFormat, $"Content Type не найден для отчёта с типом {reportFormat}")
        };    
}