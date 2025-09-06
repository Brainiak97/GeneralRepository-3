using Microsoft.AspNetCore.Mvc;

namespace CentralService.Api.Controllers
{
	[ApiController]
	[Route( "[controller]" )]
	public class OwnController : ControllerBase
	{
		[HttpGet( nameof( GetName ) )]
		public string GetName()
		{
			return "test name";
		}

		[HttpGet( nameof( GetTowns ) )]
		public IEnumerable<string> GetTowns()
		{
			return new List<string>()
			{
				"town1",
				"town2",
				"town3",
			};
		}
	}
}
