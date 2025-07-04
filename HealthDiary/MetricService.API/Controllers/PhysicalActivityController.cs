using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalActivityController(IPhysicalActivityService physicalActivityService) : Controller
    {
        readonly IPhysicalActivityService _physicalActivityService = physicalActivityService;

        [HttpPost(nameof(CreatePhysicalActivity))]
        [Authorize]
        public async Task<IActionResult> CreatePhysicalActivity([FromBody] PhysicalActivityCreateDTO physicalActivityCreateDTO)
        {
            await _physicalActivityService.CreatePhysicalActivityAsync(physicalActivityCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdatePhysicalActivity))]
        [Authorize]
        public async Task<IActionResult> UpdatePhysicalActivity([FromBody] PhysicalActivityUpdateDTO physicalActivityUpdateDTO)
        {
            await _physicalActivityService.UpdatePhysicalActivityAsync(physicalActivityUpdateDTO);
            return Ok();
        }


        [HttpDelete(nameof(DeletePhysicalActivity))]
        [Authorize]
        public async Task<IActionResult> DeletePhysicalActivity(int id)
        {
            await _physicalActivityService.DeletePhysicalActivityAsync(id);
            return Ok();
        }

        [HttpGet(nameof(GetAllPhysicalActivities))]
        public async Task<IActionResult> GetAllPhysicalActivities()
        {
            var result = await _physicalActivityService.GetAllPhysicalActivitiesAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        [HttpGet(nameof(GetPhysicalActivityById))]
        public async Task<IActionResult> GetPhysicalActivityById(int physicalActivityId)
        {
            var result = await _physicalActivityService.GetPhysicalActivityByIdAsync(physicalActivityId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet(nameof(FindPhysicalActivityByName))]
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
