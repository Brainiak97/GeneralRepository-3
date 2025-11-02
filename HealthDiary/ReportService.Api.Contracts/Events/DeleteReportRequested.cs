using Shared.Common.MessageBrokers;

namespace ReportService.Api.Contracts.Events;

/// <summary>
/// Сообщение о запросе удаления отчёта.
/// </summary>
public class DeleteReportRequested : IMessage
{
    /// <inheritdoc />
    public Guid Id { get; set; }
    
    /// <inheritdoc />
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Идентификатор отчёта.
    /// </summary>
    public int ReportId { get; set; }
}