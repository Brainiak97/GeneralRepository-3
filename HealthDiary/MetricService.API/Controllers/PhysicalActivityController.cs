using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class PhysicalActivityController(IPhysicalActivityService physicalActivityService) : Controller
    {
        readonly IPhysicalActivityService _physicalActivityService = physicalActivityService;



        [HttpGet("GetAllPhysicalActivities")]
        public async Task<IActionResult> GetAllPhysicalActivities(int pagenum, int pagesize)
        {
            var result = await _physicalActivityService.GetAllPhysicalActivitiesAsync(pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }

        [HttpGet("GetPhysicalActivityById")]
        public async Task<IActionResult> GetPhysicalActivityById(int phaid)
        {
            var result = await _physicalActivityService.GetPhysicalActivityByIdAsync(phaid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("FindPhysicalActivityByName")]
        public async Task<IActionResult> FindPhysicalActivityByName(string search)
        {
            var result = await _physicalActivityService.GetListPhysicalActivitiesBySearchAsync(search);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
