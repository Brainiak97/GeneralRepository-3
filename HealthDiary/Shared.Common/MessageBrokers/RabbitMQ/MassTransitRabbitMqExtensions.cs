using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.MessageBrokers.RabbitMQ.Configurators;
using Shared.Common.MessageBrokers.RabbitMQ.Options;
using Shared.Common.MessageBrokers.RabbitMQ.Publishers;

namespace Shared.Common.MessageBrokers.RabbitMQ;

public static class MassTransitRabbitMqExtensions
{
    public static void AddMassTransitRabbitMq(this IBusRegistrationConfigurator configurator, IConfiguration serviceConfiguration) =>
        configurator.UsingRabbitMq((context, cfg) =>
        {
            var rabbitMqOptions = serviceConfiguration.GetSection("RabbitMqOptions").Get<RabbitMqOptions>();
            if (rabbitMqOptions is not null)
            {
                cfg.Host(rabbitMqOptions.Host, config =>
                {
                    config.Username(rabbitMqOptions.Login);
                    config.Password(rabbitMqOptions.Password);
                });
            }

            cfg.ConfigureRabbitMqBus(context, serviceConfiguration);
        });

    public static void AddMessagePublisher(this IServiceCollection services) =>
        services.AddScoped<IMessagePublisher, MessagePublisher>();
}