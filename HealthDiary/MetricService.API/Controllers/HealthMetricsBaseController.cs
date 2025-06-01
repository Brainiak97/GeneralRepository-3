using MetricService.BLL.Dto;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class HealthMetricsBaseController(IHealthMetricsBaseService healthMetricsBaseService) : Controller
    {
        private readonly IHealthMetricsBaseService _healthMetricsBaseService = healthMetricsBaseService
;

        [HttpPost("SaveHealthMetricsBase")]
        public async Task<IActionResult> SaveHealthMetricsBase([FromBody] HealthMetricsBaseDTO HealthMetricsBaseDTO)
        {
            if (HealthMetricsBaseDTO.Id == 0)
            {
                return Ok(await _healthMetricsBaseService.CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO));
            }
            else
            {
                return Ok(await _healthMetricsBaseService.UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHealthMetricsBase(int id)
        {
            var responce = await _healthMetricsBaseService.DeleteRecordOfHealthMetricsBaseAsync(id);
            if (responce)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("GetAllRecordsOfHealthMetricsBase")]
        public async Task<IActionResult> GetAllRecordsOfHealthMetricsBase(int userid, DateTime begDate, DateTime endDate, int pagenum, int pagesize)
        {
            var result = await _healthMetricsBaseService.GetAllRecordsOfHealthMetricsBaseByUserIdAsync(userid, begDate, endDate, pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }


        [HttpGet("GetHealthMetricsBaseById")]
        public async Task<IActionResult> GetHealthMetricsBaseById(int hmbid)
        {
            var result = await _healthMetricsBaseService.GetRecordOfHealthMetricsBaseByIdAsync(hmbid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
