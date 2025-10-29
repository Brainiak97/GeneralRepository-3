using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

#pragma warning disable ASP0014

namespace CentralService.Api
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
			builder.Services.AddSwaggerForOcelot( builder.Configuration );

			// swagger 
			builder.Services.AddSwaggerGen( options =>
			{
				options.SwaggerDoc( "v1", new OpenApiInfo { Title = "CentralService API", Version = "v1" } );
			} );

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if ( app.Environment.IsDevelopment() )
			{
				// swagger for controllers
				//app.UseSwagger();
				//app.UseSwaggerUI();

				// swagger for ocelot
				app.UseSwaggerForOcelotUI();
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
