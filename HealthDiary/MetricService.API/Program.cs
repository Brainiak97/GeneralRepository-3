using MetricService.BLL.Interfaces;
using MetricService.BLL.Services;
using MetricService.BLL.Validators;
using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.DAL.Repositories;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Auth;
using System.Security.Claims;

namespace MetricService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавление контекста
            builder.Services.AddDbContext<MetricServiceDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(false);
            });

            // Загрузка общей конфигурации JWT
            builder.Services.AddJwtAuthentication();

            // Регистрация сервисов и репозиториев
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, MetricService.BLL.Services.UserService>();
            builder.Services.AddScoped<IValidator<User>, UserValidator>();

            builder.Services.AddScoped<ISleepRepository, SleepRepository>();
            builder.Services.AddScoped<ISleepService, SleepService>();
            builder.Services.AddScoped<IValidator<Sleep>, SleepValidator>();

            builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            builder.Services.AddScoped<IWorkoutService, WorkoutService>();
            builder.Services.AddScoped<IValidator<Workout>, WorkoutValidator>();

            builder.Services.AddScoped<IPhysicalActivityRepository, PhysicalActivityRepository>();
            builder.Services.AddScoped<IPhysicalActivityService, PhysicalActivityService>();
            builder.Services.AddScoped<IValidator<PhysicalActivity>, PhysicalActivityValidator>();

            builder.Services.AddScoped<IHealthMetricsBaseRepository, HealthMetricsBaseRepository>();
            builder.Services.AddScoped<IHealthMetricsBaseService, HealthMetricsBaseService>();
            builder.Services.AddScoped<IValidator<HealthMetricsBase>, HealtMetricBaseValidator>();

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

           
            var app = builder.Build();

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

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
