using MetricService.API;
using MetricService.API.DTO;
using MetricService.API.ExceptionHandlers;
using MetricService.BLL.Common;
using MetricService.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Auth;
using Shared.Logging;
using System.Security.Claims;

namespace MetricService.Api
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// точка входа в приложение
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Настройка логгера
            LoggingConfiguration.ConfigureLogger(
            serviceName: "MetricService",
            layer: "API",
            builder.Configuration,
            environment: builder.Environment.EnvironmentName);

            builder.Host.UseSerilog();

            // Добавление контекста
            builder.Services.AddDbContext<MetricServiceDbContext>(options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                    options.EnableSensitiveDataLogging(false);
                });

            // Загрузка общей конфигурации JWT
            builder.Services.AddJwtAuthentication();

            // Регистрация сервисов и репозиториев
            EntitiesServices.Register(builder);


            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ClaimsPrincipal>(x =>
            {
                var context = x.GetRequiredService<IHttpContextAccessor>();
                return context.HttpContext!.User;
            });
            builder.Services.AddSingleton<IJwtService, JwtService>();

            // Остальные сервисы
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();



            // Настройка свагера            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricService API", Version = "v1" });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MetricService.Api.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MetricService.BLL.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MetricService.DAL.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MetricService.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Shared.Auth.xml"));

                // Добавляем схему JWT в Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<BaseExceptionHandler>();
            builder.Services.AddAutoMapper(cfg => cfg.AddProfiles([new MapperProfile(), new ApiMapperProfile()]));

            var app = builder.Build();

            app.UseExceptionHandler();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MetricServiceDbContext>();

                    // Применение миграций
                    await context.Database.MigrateAsync();

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ошибка при инициализации БД");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCommonRequestLogging();

            app.UseMiddleware<CorrelationIdMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
