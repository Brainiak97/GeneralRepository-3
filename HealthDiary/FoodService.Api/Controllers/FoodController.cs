using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team3.HealthDiary.FoodService.BLL.Interfaces;
using Team3.HealthDiary.FoodService.DAL;

namespace Team3.HealthDiary.FoodService.Api.Controllers
{
	[ApiController]
	[Route( "[controller]" )]
	public class FoodController : ControllerBase
	{
		private FoodServiceDbContext _dbContext;
		private readonly IMapper _modelMapper;
		private readonly IFoodService _foodService;

		public FoodController( FoodServiceDbContext dbContext, IMapper modelMapper, IFoodService foodService )
		{
			_dbContext = dbContext;
			_modelMapper = modelMapper;
			_foodService = foodService;
		}

		// to test
		[HttpGet( nameof( GetProduct ) )]
		public async Task<IActionResult> GetProduct( int productId )
		{
			var product = await _dbContext.Products.FirstOrDefaultAsync( x => x.Id == productId );
			if ( product != null )
			{
				return Ok( product );
			}
			else
			{
				return NotFound();
			}
		}

		// to test
		[HttpGet( nameof( GetTestError ) )]
		public async Task<string> GetTestError()
		{
			throw new Exception( "test exc" );
			return "test str";
		}
	}
}
