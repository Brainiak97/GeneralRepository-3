using ReportService.Domain.Models;

namespace ReportService.BLL.Data.Commands;

/// <summary>
/// Интерфейс команды сервиса.
/// </summary>
public interface IServiceCommand
{
    /// <summary>
    /// Идентификатор отчёта.
    /// </summary>
    int ReportId { get; init; }

    /// <summary>
    /// Формат отчёта.
    /// </summary>
    ReportFormat ReportFormat { get; init; }
    
    /// <summary>
    /// Имя файла (с расширением).
    /// </summary>
    string FileName { get; init; }

    /// <summary>
    /// Содержимое отчёта.
    /// </summary>
    byte[] Content { get; init; }
}