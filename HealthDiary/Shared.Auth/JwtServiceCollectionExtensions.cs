using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Shared.Auth
{
    /// <summary>
    /// Содержит методы расширения для регистрации JWT-аутентификации в контейнере зависимостей.
    /// </summary>
    public static class JwtServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет настройки JWT-аутентификации в коллекцию сервисов.
        /// Ожидает наличие встроенного ресурса jwt-config.json в проекте Shared.Auth.
        /// </summary>
        /// <param name="services">Коллекция сервисов для регистрации.</param>
        /// <returns>Обновлённая коллекция сервисов.</returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            // Чтение jwt-config.json как встроенного ресурса
            var assembly = Assembly.GetAssembly(typeof(JwtSettings));
            var resourceName = "Shared.Auth.jwt-config.json";

            using var stream = (assembly?.GetManifestResourceStream(resourceName))
                ?? throw new InvalidOperationException("Файл конфигурации JWT не найден");
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            var jwtSettings = JsonConvert.DeserializeObject<JwtSettings>(json)
                ?? throw new InvalidOperationException("Ошибка десериализации jwt-config.json");

            // Регистрация JwtSettings как singleton
            services.AddSingleton(jwtSettings);

            services.AddAuthorizationBuilder()
                .AddPolicy("SelfOrAdmin", policy =>
                    policy.RequireAssertion(context =>
                    {
                        var httpContext = context.Resource as HttpContext
                            ?? throw new InvalidOperationException("Expected resource to be an HttpContext.");

                        var routeData = httpContext.GetRouteData()
                            ?? throw new InvalidOperationException("Route data is null.");

                        var userId = routeData.Values["id"]?.ToString();

                        if (string.IsNullOrEmpty(userId))
                            return false;

                        var user = httpContext.User;

                        var currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                        var isAdmin = user.IsInRole("Admin");

                        return currentUserId == userId || isAdmin;
                    }));

            // Настройка аутентификации
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}