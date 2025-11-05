using ReportService.Api.Contracts.Enums;
using Shared.Common.MessageBrokers;

namespace ReportService.Api.Contracts.Events;

/// <summary>
/// Сообщение о запросе генерации отчёта.
/// </summary>
public class GenerateReportRequested : IMessage
{
    /// <inheritdoc />
    public Guid Id { get; set; }
    
    /// <inheritdoc />
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public int EntityId { get; set; }
    
    /// <summary>
    /// Содержимое отчёта.
    /// </summary>
    public required string ReportContent { get; set; }
    
    /// <summary>
    /// Формат отчёта.
    /// </summary>
    public ReportFormat ReportFormat { get; set; }
    
    /// <summary>
    /// Идентификатор шаблона отчёта.
    /// </summary>
    public int ReportTemplateId { get; set; }
    
    /// <summary>
    /// Признак необходимости отправки отчёта по почте.
    /// </summary>
    public bool NeedSendToEmail { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string? EmailAddress { get; set; }
}