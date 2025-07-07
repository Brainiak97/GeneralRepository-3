
using FoodService.DAL;
using Microsoft.EntityFrameworkCore;

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

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if ( app.Environment.IsDevelopment() )
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.Services.ApplyDbMigration();

			app.MapControllers();

			app.Run();
		}
	}
}
