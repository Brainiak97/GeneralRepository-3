using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с даннымии базовых медицинских показателей пользователя
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HealthMetricsBaseController(IHealthMetricsBaseService healthMetricsBaseService) : Controller
    {
        private readonly IHealthMetricsBaseService _healthMetricsBaseService = healthMetricsBaseService;

        /// <summary>
        /// Зарегистрировать базовы медицинские показатели пользователя
        /// </summary>
        /// <param name="HealthMetricsBaseDTO">Данные базовых медицинских показателей пользователя для регистрации</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateHealthMetricsBase))]
        public async Task<IActionResult> CreateHealthMetricsBase([FromBody] HealthMetricsBaseCreateDTO HealthMetricsBaseDTO)
        {
            await _healthMetricsBaseService.CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о базовых медицинских показателях пользователя 
        /// </summary>
        /// <param name="HealthMetricsBaseDTO">Измененные данные базовых медицинских показателей пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateHealthMetricsBase))]
        public async Task<IActionResult> UpdateHealthMetricsBase([FromBody] HealthMetricsBaseUpdateDTO HealthMetricsBaseDTO)
        {
            await _healthMetricsBaseService.UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить данны о базовых медицинских показателях пользователя
        /// </summary>
        /// <param name="healthMetricsBaseId">Идентификатор базовых медицинских показателей пользователя</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteHealthMetricsBase))]
        public async Task<IActionResult> DeleteHealthMetricsBase(int healthMetricsBaseId)
        {
            await _healthMetricsBaseService.DeleteRecordOfHealthMetricsBaseAsync(healthMetricsBaseId);
            return Ok();
        }

        /// <summary>
        /// Подучить список базовых медицинских показателей пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
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

        /// <summary>
        /// получить данные о базовых медицинских показателях пользователя
        /// </summary>
        /// <param name="healthMetricsBaseId">идентификатор данных о базовых медицинских показателях</param>
        /// <returns></returns>
        [HttpGet(nameof(GetHealthMetricsBaseById))]
        public async Task<IActionResult> GetHealthMetricsBaseById(int healthMetricsBaseId)
        {
            return Ok(await _healthMetricsBaseService.GetRecordOfHealthMetricsBaseByIdAsync(healthMetricsBaseId));
        }
    }
}
