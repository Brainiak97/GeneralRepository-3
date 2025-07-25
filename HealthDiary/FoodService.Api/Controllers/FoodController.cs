using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Team3.HealthDiary.FoodService.Api.Controllers
{
	[ApiController]
	[Route( "[controller]" )]
	public class FoodController : ControllerBase
	{
		private FoodServiceDbContext _dbContext;
		private readonly IMapper _modelMapper;

		public FoodController( FoodServiceDbContext dbContext )
		public FoodController( FoodServiceDbContext dbContext, IMapper modelMapper )
		{
			_dbContext = dbContext;
			_modelMapper = modelMapper;
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
