using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.Exceptions;
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


        [HttpPost("CreateSleep")]
        public async Task<IActionResult> CreateSleep([FromBody] SleepCreateDTO sleepDTO)
        {
            try
            {
                await _sleepService.CreateRecordOfSleepAsync(sleepDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpPost("UpdateSleep")]
        public async Task<IActionResult> UpdateSleep([FromBody] SleepUpdateDTO sleepDTO)
        {
            try
            {
                await _sleepService.UpdateRecordOfSleepAsync(sleepDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpDelete("DeleteSleep")]
        public async Task<IActionResult> DeleteSleep(int id)
        {
            try
            {
                await _sleepService.DeleteRecordOfSleepAsync(id);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpGet("GetAllSleeps")]       
        public async Task<IActionResult> GetAllSleeps([FromQuery]RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            try
            {
                var result = await _sleepService.GetAllRecordsOfSleepByUserIdAsync(requestListWithPeriodByIdDTO);

                if (!result.Any())
                {
                    return Ok("Список пуст");
                }

                return Ok(result.ToArray());
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpGet("GetSleepById")]
        public async Task<IActionResult> GetSleepById(int sleepid)
        {
            try
            {
                return Ok(await _sleepService.GetRecordOfSleepByIdAsync(sleepid));
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }
    }
}
