using MassTransit;
using Microsoft.Extensions.Configuration;
using Shared.Common.MessageBrokers.RabbitMQ.Options;

namespace Shared.Common.MessageBrokers.RabbitMQ.Configurators;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureRabbitMqBus(
        this IRabbitMqBusFactoryConfigurator configurator,
        IBusRegistrationContext context,
        IConfiguration serviceConfiguration)
    {
        configurator.SetConcurrencyLimitAndPrefetchCount(serviceConfiguration);
        configurator.ConfigureEndpoints(context);
    }

    private static void SetConcurrencyLimitAndPrefetchCount(
        this IRabbitMqBusFactoryConfigurator configurator,
        IConfiguration serviceConfiguration)
    {
        var busOptions = serviceConfiguration.GetSection("BusOptions").Get<BusOptions>();

        // Если в конфиге ничего нет, то сообщения обрабатываются одним консьюмером по одному сообщению
        configurator.UseConcurrencyLimit(busOptions?.ConcurrencyLimit ?? 1);
        configurator.PrefetchCount = busOptions?.PrefetchCount ?? 1;
    }
}