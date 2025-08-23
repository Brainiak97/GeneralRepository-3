using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Team3.HealthDiary.FoodService.BLL.Interfaces;
using Team3.HealthDiary.FoodService.DAL;
using Team3.HealthDiary.Shared.Common.Middlewares;

namespace Team3.HealthDiary.FoodService.Api
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

			builder.Services.AddSwaggerGen( options =>
			{
				options.SwaggerDoc( "v1", new OpenApiInfo() { Title = "FoodService.Api", Version = "v1" } );
			} );

			builder.Services.AddAutoMapper( x => x.AddProfile<AutoMapperProfile>() );

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
