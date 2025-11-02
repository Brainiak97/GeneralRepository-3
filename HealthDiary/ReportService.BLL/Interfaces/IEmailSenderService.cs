using ReportService.Domain.Models;

namespace ReportService.BLL.Interfaces;

/// <summary>
/// Предоставляет методы для отправки данных в по электронной почте через EmailService.
/// </summary>
public interface IEmailSendService
{
    /// <summary>
    /// Отправить отчёт.
    /// </summary>
    /// <param name="reportId">Идентификатор отчёта.</param>
    /// <param name="emailAddress">Адрес электронной почты.</param>
    /// <param name="report">Содержимое отчёта.</param>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="reportFormat">Формат отчёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task SendReportAsync(
        int reportId,
        string emailAddress,
        byte[] report,
        string fileName,
        ReportFormat reportFormat,
        CancellationToken cancellationToken = default);    
}