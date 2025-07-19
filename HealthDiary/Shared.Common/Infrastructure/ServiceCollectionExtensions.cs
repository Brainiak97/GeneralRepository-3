using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Shared.Common.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerOptions? swaggerOptions, string? serviceName)
    {
        ArgumentException.ThrowIfNullOrEmpty(serviceName);
        ArgumentNullException.ThrowIfNull(swaggerOptions);

        services.AddSwaggerGen(options =>
        {

            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = swaggerOptions.Title,
                    Description = swaggerOptions.Description,
                    Version = "v1",
                    License = swaggerOptions.License,
                    TermsOfService = swaggerOptions.TermsOfService,
                    Contact = swaggerOptions.Contact,
                });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{serviceName}.Api.xml"));
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{serviceName}.BLL.xml"));
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{serviceName}.Domain.xml"));

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

        return services;
    }
}