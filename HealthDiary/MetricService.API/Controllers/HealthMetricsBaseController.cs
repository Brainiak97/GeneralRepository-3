using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.BLL.Exceptions;
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
        private readonly IHealthMetricsBaseService _healthMetricsBaseService = healthMetricsBaseService;

        [HttpPost(nameof(CreateHealthMetricsBase))]
        public async Task<IActionResult> CreateHealthMetricsBase([FromBody] HealthMetricsBaseCreateDTO HealthMetricsBaseDTO)
        {
            try
            {
                await _healthMetricsBaseService.CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO);
                return Ok();
            }catch(BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
           
        }

        [HttpPost(nameof(UpdateHealthMetricsBase))]
        public async Task<IActionResult> UpdateHealthMetricsBase([FromBody] HealthMetricsBaseUpdateDTO HealthMetricsBaseDTO)
        {
            try
            {
                await _healthMetricsBaseService.UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO);
                return Ok();
            }catch(BaseException ex)
            {
                return BadRequest(ex.GetError());
            }            
        }


        [HttpDelete(nameof(DeleteHealthMetricsBase))]
        public async Task<IActionResult> DeleteHealthMetricsBase(int id)
        {
            try
            {
                await _healthMetricsBaseService.DeleteRecordOfHealthMetricsBaseAsync(id);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpGet(nameof(GetAllRecordsOfHealthMetricsBase))]
        public async Task<IActionResult> GetAllRecordsOfHealthMetricsBase([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            try
            {
                var result = await _healthMetricsBaseService.GetAllRecordsOfHealthMetricsBaseByUserIdAsync(requestListWithPeriodByIdDTO);

                if (!result.Any())
                {
                    return Ok("Список пуст");
                }

                return Ok(result);
            }catch(BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpGet(nameof(GetHealthMetricsBaseById))]
        public async Task<IActionResult> GetHealthMetricsBaseById(int healthMetricsBaseId)
        {
            try
            {

                return Ok(await _healthMetricsBaseService.GetRecordOfHealthMetricsBaseByIdAsync(healthMetricsBaseId));
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }
    }
}
