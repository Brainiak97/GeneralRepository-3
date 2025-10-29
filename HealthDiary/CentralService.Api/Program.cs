using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Shared.Auth;

#pragma warning disable ASP0014

namespace CentralService.Api
{
	public class Program
	{
		public static void Main( string[] args )
		{
			var builder = WebApplication.CreateBuilder( args );

            // Загрузка общей конфигурации JWT
            builder.Services.AddJwtAuthentication();

            builder.Services.AddControllers();

			// ocelot
			builder.Configuration.AddJsonFile( "OcelotRouting.json" );
			builder.Services.AddOcelot();
			builder.Services.AddSwaggerForOcelot( builder.Configuration );

			// swagger 
			builder.Services.AddSwaggerGen( options =>
			{
				options.SwaggerDoc( "v1", new OpenApiInfo { Title = "CentralService API", Version = "v1" } );

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
            } );

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if ( app.Environment.IsDevelopment() )
			{
				// swagger for ocelot
				app.UseSwaggerForOcelotUI();
			}

            // controllers
            app.UseRouting();
			app.UseEndpoints( endpoints =>
			{
				endpoints.MapControllers();
			} );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOcelot().Wait();

			app.Run();
		}
	}
}
