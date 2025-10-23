namespace Shared.Common.MessageBrokers.RabbitMQ.Options;

/// <summary>
/// Параметры конфигурации для подключения к RabbitMq.
/// </summary>
/// <param name="Host">Имя хоста.</param>
/// <param name="Login">Логин.</param>
/// <param name="Password">Пароль.</param>
public record RabbitMqOptions(
    string Host,
    string Login,
    string Password);