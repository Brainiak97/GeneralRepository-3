using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Shared.Logging
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogger(string serviceName, string layer, IConfiguration serviceConfiguration, string environment = "Development")
        {
            // 1. Загружаем ОБЩИЙ конфиг из shared-проекта
            var baseConfig = new ConfigurationBuilder()
                .AddJsonFile("logging.settings.json", optional: false, reloadOnChange: false)
                .Build();


            // 2. Читаем Seq-настройки — сначала из общего, потом переопределяем из текущего
            var seqHost = serviceConfiguration["Seq:Host"]
                          ?? baseConfig["Seq:Host"]
                          ?? throw new InvalidOperationException("Seq:Host is required.");

            var seqApiKey = serviceConfiguration["Seq:ApiKey"] ?? baseConfig["Seq:ApiKey"];
            var seqFilePath = serviceConfiguration["Seq:FilePath"] ?? baseConfig["Seq:FilePath"] ?? "./logs";
            var retainedFileCountLimit = int.Parse(serviceConfiguration["Seq:RetainedFileCountLimit"]
                                                   ?? baseConfig["Seq:RetainedFileCountLimit"]
                                                   ?? "7");

            // 5. Уровень логирования по среде
            var levelSwitch = new LoggingLevelSwitch
            {
                MinimumLevel = environment switch
                {
                    "Development" => LogEventLevel.Debug,
                    "Staging" => LogEventLevel.Information,
                    "Production" => LogEventLevel.Information,
                    _ => LogEventLevel.Information
                }
            };

            // 6. Создаём логгер
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)

                .Enrich.FromLogContext()
                .Enrich.WithProperty("ServiceName", serviceName)
                .Enrich.WithProperty("Layer", layer)
                .Enrich.WithProperty("Environment", environment)

                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")

                .WriteTo.File(
                    path: Path.Combine(seqFilePath, $"{serviceName}-{layer}-log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: retainedFileCountLimit)

                .WriteTo.Seq(seqHost, apiKey: seqApiKey)

                .CreateLogger();
        }
    }
}
