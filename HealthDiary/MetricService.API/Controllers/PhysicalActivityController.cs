using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class PhysicalActivityController(IPhysicalActivityService physicalActivityService) : Controller
    {
        readonly IPhysicalActivityService _physicalActivityService = physicalActivityService;


        [HttpPost("CreatePhysicalActivity")]
        public async Task<IActionResult> CreatePhysicalActivity([FromBody] PhysicalActivityDTO physicalActivityDTO)
        {
            try
            {
                await _physicalActivityService.CreatePhysicalActivityAsync(physicalActivityDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpPost("UpdatePhysicalActivity")]
        public async Task<IActionResult> UpdatePhysicalActivity([FromBody] PhysicalActivityDTO physicalActivityDTO)
        {
            try
            {
                await _physicalActivityService.UpdatePhysicalActivityAsync(physicalActivityDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpDelete("DeletePhysicalActivity")]
        public async Task<IActionResult> DeletePhysicalActivity(int id)
        {
            try
            {
                await _physicalActivityService.DeletePhysicalActivityAsync(id);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

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
        public async Task<IActionResult> GetPhysicalActivityById(int physicalActivityId)
        {
            try
            {
                var result = await _physicalActivityService.GetPhysicalActivityByIdAsync(physicalActivityId);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }catch(BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
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
