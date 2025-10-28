using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.MessageBrokers.RabbitMQ;

namespace Shared.Common.MessageBrokers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        // Регистрируем консьюмеров из слоя бизнес-логики
        var consumerAssemblies = FindConsumerAssemblies();

        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumers(consumerAssemblies);
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddMassTransitRabbitMq(configuration);
        });

        services.AddMessagePublisher();

        return services;
    }

    private static Assembly[] FindConsumerAssemblies() =>
        AppDomain.CurrentDomain.GetAssemblies()?.Where(a => a.FullName is not null && a.FullName.Contains(".BLL")).ToArray()
            ?? [];
}