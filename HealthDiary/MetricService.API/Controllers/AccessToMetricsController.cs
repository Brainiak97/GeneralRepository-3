using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AccessToMetrics;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данными доступа к личным метрикам пользователя
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class AccessToMetricsController(IAccessToMetricsService accessToMetricsService) : Controller
    {
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;

        /// <summary>
        /// Предоставить доступ к личным метрикам
        /// </summary>
        /// <param name="accessToMetricsCreateDTO">Данные для предоставления доступа</param>        
        /// <returns></returns>
        [HttpPost(nameof(CreateAccessToMetricsAsync))]
        public async Task<IActionResult> CreateAccessToMetricsAsync([FromBody] AccessToMetricsCreateDTO accessToMetricsCreateDTO)
        {
            await _accessToMetricsService.CreateAccessToMetricsAsync(accessToMetricsCreateDTO);
            return Ok();
        }


        /// <summary>
        /// Изменить доступ к личным метрикам
        /// </summary>
        /// <param name="accessToMetricsUpdateDTO">Данные для изменения доступа к личным метрикам</param>        
        /// <returns></returns>
        [HttpPut(nameof(UpdateAccessToMetricsAsync))]
        public async Task<IActionResult> UpdateAccessToMetricsAsync([FromBody] AccessToMetricsUpdateDTO accessToMetricsUpdateDTO)
        {
            await _accessToMetricsService.UpdateAccessToMetricsAsync(accessToMetricsUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить доступ к личным метрикам
        /// </summary>
        /// <param name="accessToMetricsId">Идентификатор записи доступа</param>       
        /// <returns></returns>
        [HttpDelete(nameof(DeleteAccessToMetricsAsync))]
        public async Task<IActionResult> DeleteAccessToMetricsAsync(int accessToMetricsId)
        {
            await _accessToMetricsService.DeleteAccessToMetricsAsync(accessToMetricsId);
            return Ok();
        }

        /// <summary>
        /// Получить список доступа к личным метрикам для пользователя, предоставившего доступ
        /// </summary>
        /// <param name="requestAccessListWithPeriodByIdDTO">Данные пользователя, период и типы записей</param>       
        /// <returns></returns>
        [HttpGet(nameof(GetAllAccessToMetricsByProviderAsync))]
        public async Task<IActionResult> GetAllAccessToMetricsByProviderAsync([FromQuery] RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO)
        {
            var result = await _accessToMetricsService.GetAllAccessToMetricsByProviderUserIdAsync(requestAccessListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }


        /// <summary>
        /// Получить список доступа к личным метрикам для пользователя, получившего доступ
        /// </summary>
        /// <param name="requestAccessListWithPeriodByIdDTO">Данные пользователя, период и типы записей</param>        
        /// <returns></returns>
        [HttpGet(nameof(GetAllAccessToMetricsByGrantedAsync))]
        public async Task<IActionResult> GetAllAccessToMetricsByGrantedAsync([FromQuery] RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO)
        {
            var result = await _accessToMetricsService.GetAllAccessToMetricsByGrantedUserIdAsync(requestAccessListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }
    }
}
