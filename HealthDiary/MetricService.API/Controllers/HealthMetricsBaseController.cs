using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HealthMetricsBaseController(IHealthMetricsBaseService healthMetricsBaseService) : Controller
    {
        private readonly IHealthMetricsBaseService _healthMetricsBaseService = healthMetricsBaseService;

        [HttpPost(nameof(CreateHealthMetricsBase))]
        public async Task<IActionResult> CreateHealthMetricsBase([FromBody] HealthMetricsBaseCreateDTO HealthMetricsBaseDTO)
        {
            await _healthMetricsBaseService.CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateHealthMetricsBase))]
        public async Task<IActionResult> UpdateHealthMetricsBase([FromBody] HealthMetricsBaseUpdateDTO HealthMetricsBaseDTO)
        {
            await _healthMetricsBaseService.UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO);
            return Ok();
        }


        [HttpDelete(nameof(DeleteHealthMetricsBase))]
        public async Task<IActionResult> DeleteHealthMetricsBase(int id)
        {
            await _healthMetricsBaseService.DeleteRecordOfHealthMetricsBaseAsync(id);
            return Ok();
        }


        [HttpGet(nameof(GetAllRecordsOfHealthMetricsBase))]
        public async Task<IActionResult> GetAllRecordsOfHealthMetricsBase([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _healthMetricsBaseService.GetAllRecordsOfHealthMetricsBaseByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }


        [HttpGet(nameof(GetHealthMetricsBaseById))]
        public async Task<IActionResult> GetHealthMetricsBaseById(int healthMetricsBaseId)
        {
            return Ok(await _healthMetricsBaseService.GetRecordOfHealthMetricsBaseByIdAsync(healthMetricsBaseId));
        }
    }
}
