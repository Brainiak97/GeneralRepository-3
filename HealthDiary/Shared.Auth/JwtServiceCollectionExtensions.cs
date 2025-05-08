using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace Shared.Auth
{
    public static class JwtServiceCollectionExtensions
    {
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
