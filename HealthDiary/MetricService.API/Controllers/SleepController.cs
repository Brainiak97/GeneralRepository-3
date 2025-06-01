using MetricService.BLL.Dto;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SleepController(ISleepService sleepService) : Controller
    {
        private readonly ISleepService _sleepService = sleepService;


        [HttpPost("SaveSleep")]
        public async Task<IActionResult> SaveSleep([FromBody] SleepDTO sleepDTO)
        {
            if (sleepDTO.Id == 0)
            {
                return Ok(await _sleepService.CreateRecordOfSleepAsync(sleepDTO));
            }
            else
            {
                return Ok(await _sleepService.UpdateRecordOfSleepAsync(sleepDTO));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSleep(int id)
        {
            var responce = await _sleepService.DeleteRecordOfSleepAsync(id);
            if (responce)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllSleeps")]
        public async Task<IActionResult> GetAllSleeps(int userid, DateTime begDate, DateTime endDate, int pagenum, int pagesize)
        {
            var result = await _sleepService.GetAllRecordsOfSleepByUserIdAsync(userid, begDate, endDate, pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }


        [HttpGet("GetSleepById")]
        public async Task<IActionResult> GetSleepById(int sleepid)
        {
            var result = await _sleepService.GetRecordOfSleepByIdAsync(sleepid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
