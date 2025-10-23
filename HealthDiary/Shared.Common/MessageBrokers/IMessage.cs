using MassTransit;

namespace Shared.Common.MessageBrokers;

/// <summary>
/// Модель сообщения для отправки в брокер.
/// </summary>
[ExcludeFromTopology]
public interface IMessage
{
    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    Guid Id { get; set; }

    /// <summary>
    /// Дата сообщения.
    /// </summary>
    DateTime Timestamp { get; set; }
}