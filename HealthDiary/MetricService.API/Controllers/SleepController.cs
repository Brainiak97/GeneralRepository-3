using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SleepController(ISleepService sleepService) : Controller
    {
        private readonly ISleepService _sleepService = sleepService;


        [HttpPost(nameof(CreateSleep))]
        public async Task<IActionResult> CreateSleep([FromBody] SleepCreateDTO sleepDTO)
        {
            await _sleepService.CreateRecordOfSleepAsync(sleepDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateSleep))]
        public async Task<IActionResult> UpdateSleep([FromBody] SleepUpdateDTO sleepDTO)
        {
            await _sleepService.UpdateRecordOfSleepAsync(sleepDTO);
            return Ok();
        }


        [HttpDelete(nameof(DeleteSleep))]
        public async Task<IActionResult> DeleteSleep(int id)
        {
            await _sleepService.DeleteRecordOfSleepAsync(id);
            return Ok();
        }

        [HttpGet(nameof(GetAllSleeps))]
        public async Task<IActionResult> GetAllSleeps([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _sleepService.GetAllRecordsOfSleepByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }


        [HttpGet(nameof(GetSleepById))]
        public async Task<IActionResult> GetSleepById(int sleepid)
        {
            return Ok(await _sleepService.GetRecordOfSleepByIdAsync(sleepid));
        }
    }
}
