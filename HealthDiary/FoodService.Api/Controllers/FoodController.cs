using FoodService.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Api.Controllers
{
	[ApiController]
	[Route( "[controller]" )]
	public class FoodController : ControllerBase
	{
		private FoodServiceDbContext _dbContext;

		public FoodController( FoodServiceDbContext dbContext )
		{
			_dbContext = dbContext;
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
