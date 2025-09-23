using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FoodService.BLL.Interfaces;
using FoodService.DAL;
using FoodService.DAL.Repository;
using Shared.Common.Middlewares;

namespace FoodService.Api
{
	public class Program
	{
		public static void Main( string[] args )
		{
			var builder = WebApplication.CreateBuilder( args );

			var foodServiceDbConnectionString = builder.Configuration.GetConnectionString( "FoodServiceDbConnectionString" );

			// Add services to the container.
			builder.Services.AddPgFoodServiceDbContext( foodServiceDbConnectionString! );
			builder.Services.AddTransient<IFoodRepository, FoodRepository>();
			builder.Services.AddAutoMapper( x => x.AddProfile<AutoMapperProfile>() );

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			builder.Services.AddSwaggerGen( options =>
			{
				options.SwaggerDoc( "v1", new OpenApiInfo() { Title = "FoodService.Api", Version = "v1" } );
			} );

			// Add services
			builder.Services.AddScoped<IFoodService, BLL.Services.FoodService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if ( app.Environment.IsDevelopment() )
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.Services.ApplyDbMigration();

			app.MapControllers();

			app.UseMiddleware<ErrorHandlerMiddleware>();

			app.Run();
		}
	}
}
