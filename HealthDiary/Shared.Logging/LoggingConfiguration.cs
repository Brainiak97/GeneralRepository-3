using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Shared.Logging
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogger(string serviceName, string layer, IConfiguration configuration, string environment = "Development")
        {
            var seqApiKey = configuration["Seq:ApiKey"];
            if (string.IsNullOrEmpty(seqApiKey))
            {
                throw new InvalidOperationException("Seq API Key is missing. Set 'Seq:ApiKey' in secrets or environment variables.");
            }

            var levelSwitch = new LoggingLevelSwitch
            {
                // Установи уровень по environment
                MinimumLevel = environment switch
                {
                    "Development" => LogEventLevel.Debug,
                    "Staging" => LogEventLevel.Information,
                    "Production" => LogEventLevel.Information,
                    _ => LogEventLevel.Information
                }
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)

                // Переопределения
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)

                // Обогащение
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ServiceName", serviceName)
                .Enrich.WithProperty("Layer", layer)
                .Enrich.WithProperty("Environment", environment)

                // Выводы
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .WriteTo.File($"logs/{serviceName}-{layer}-log-.txt", rollingInterval: RollingInterval.Day)

                // Например, Seq
                .WriteTo.Seq("http://localhost:5341/", apiKey: "ZiJbkAaEBgh8clRECXwG")

                .CreateLogger();
        }
    }
}
