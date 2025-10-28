namespace Shared.Common.MessageBrokers.RabbitMQ.Publishers;

public interface IMessagePublisher
{
    /// <summary>
    /// Опубликовать сообщение в брокере.
    /// </summary>
    /// <param name="message">Публикуемое сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <typeparam name="TMessage"></typeparam>
    /// <returns></returns>
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : IMessage;
}