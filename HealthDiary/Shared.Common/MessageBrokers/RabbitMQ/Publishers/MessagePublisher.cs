using MassTransit;

namespace Shared.Common.MessageBrokers.RabbitMQ.Publishers;

/// <inheritdoc/>
internal class MessagePublisher(IPublishEndpoint publishEndpoint) : IMessagePublisher
{
    /// <inheritdoc/>
    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken) where TMessage : IMessage
    {
        ArgumentNullException.ThrowIfNull(message);
        SetMetadata(message);
        await publishEndpoint.Publish(message, cancellationToken);
    }

    private static void SetMetadata<TMessage>(TMessage message) where TMessage : IMessage
    {
        message.Id = Guid.NewGuid();
        message.Timestamp = DateTime.UtcNow;
    }
}