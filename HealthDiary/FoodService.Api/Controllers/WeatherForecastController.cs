using FoodService.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Api.Controllers
{
	// TODO to delete
	[ApiController]
	[Route( "[controller]" )]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly FoodServiceDbContext _context;

		public WeatherForecastController( ILogger<WeatherForecastController> logger, FoodServiceDbContext context )
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet( Name = "GetWeatherForecast" )]
		public IEnumerable<WeatherForecast> Get()
		{
			//var pr = _context.MealItems
			//	.Include( x => x.Meal )
			//	.Include( x => x.Product )
			//	.ToList();

			var pr = _context.MealItems.ToList();
			//_context.Products.Load();

			foreach ( var item in pr )
			{
				Console.WriteLine( item.Id );
				Console.WriteLine( item.MealId );
				Console.WriteLine( item.ProductId );
				Console.WriteLine( item.Product );
				Console.WriteLine( item.Meal );
				Console.WriteLine();
			}

			return Enumerable.Range( 1, 5 ).Select( index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime( DateTime.Now.AddDays( index ) ),
				TemperatureC = Random.Shared.Next( -20, 55 ),
				Summary = Summaries[Random.Shared.Next( Summaries.Length )]
			} )
			.ToArray();
		}
	}
}
