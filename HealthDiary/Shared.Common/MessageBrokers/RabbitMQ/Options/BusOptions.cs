namespace Shared.Common.MessageBrokers.RabbitMQ.Options;

/// <summary>
/// Параметры конфигурации для настройки обработки сообщений от RabbitMq.
/// </summary>
/// <param name="ConcurrencyLimit">Количество консьюмеров параллельно обрабатывающих сообщения.</param>
/// <param name="PrefetchCount">Количество выгружаемых сообщений из очереди.</param>
public record BusOptions(short? ConcurrencyLimit, short? PrefetchCount);