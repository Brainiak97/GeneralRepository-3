using ReportService.Domain.Models;

namespace ReportService.BLL.Data.Commands;

/// <summary>
/// Команда редактирования сгенерированного отчёта.
/// </summary>
public record UpdateReportCommand : IServiceCommand
{
    /// <inheritdoc />
    public int ReportId { get; init; }

    /// <inheritdoc />
    public ReportFormat ReportFormat { get; init; }

    /// <inheritdoc />
    public required string FileName { get; init; }

    /// <inheritdoc />
    public byte[] Content { get; init; } = [];
}