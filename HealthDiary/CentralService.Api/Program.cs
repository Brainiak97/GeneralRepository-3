using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

#pragma warning disable ASP0014

namespace Team3.HealthDiary.CentralService.Api
{
	public class Program
	{
		public static void Main( string[] args )
		{
			var builder = WebApplication.CreateBuilder( args );

			builder.Services.AddControllers();

			// ocelot
			builder.Configuration.AddJsonFile( "OcelotRouting.json" );
			builder.Services.AddOcelot();

			// swagger 
			builder.Services.AddSwaggerGen( options =>
			{
				options.SwaggerDoc( "v1", new OpenApiInfo { Title = "CentralService API", Version = "v1" } );
			} );

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if ( app.Environment.IsDevelopment() )
			{
				// swagger 
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// controllers
			app.UseRouting();
			app.UseEndpoints( endpoints =>
			{
				endpoints.MapControllers();
			} );

			app.UseOcelot();

			app.Run();
		}
	}
}
