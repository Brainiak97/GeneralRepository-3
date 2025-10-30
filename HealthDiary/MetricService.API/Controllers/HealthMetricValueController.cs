using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetric;
using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с показателями здоровья пользователя
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class HealthMetricValueController(IHealthMetricValueService healthMetricValueService, IMapper mapper) : Controller
    {
        private readonly IHealthMetricValueService _healthMetricValueService = healthMetricValueService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Зарегистрировать новое значение показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueCreateDTO">Данные для регистрации нового значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateHealthMetricValue))]
        [Authorize]
        public async Task<IActionResult> CreateHealthMetricValue([FromBody] HealthMetricValueCreateDTO healthMetricValueCreateDTO)
        {
            await _healthMetricValueService.CreateHealthMetricValueAsync(_mapper.Map<HealthMetricValue>(healthMetricValueCreateDTO));
            return Ok();
        }

        /// <summary>
        /// Изменить данные о значении показателе здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueUpdateDTO">Данные для изменения значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateHealthMetricValue))]
        [Authorize]
        public async Task<IActionResult> UpdateHealthMetricValue([FromBody] HealthMetricValueUpdateDTO healthMetricValueUpdateDTO)
        {
            await _healthMetricValueService.UpdateHealthMetricValueAsync(_mapper.Map<HealthMetricValue>(healthMetricValueUpdateDTO));
            return Ok();
        }

        /// <summary>
        /// Удалить данные о значении показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueId">Идентификатор значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteHealthMetricValue))]
        [Authorize]
        public async Task<IActionResult> DeleteHealthMetricValue(int healthMetricValueId)
        {
            await _healthMetricValueService.DeleteHealthMetricValueAsync(healthMetricValueId);
            return Ok();
        }

        /// <summary>
        /// Получить список значений показателей здоровья пользователя
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllHealthMetricsValue))]
        [Authorize]
        public async Task<IActionResult> GetAllHealthMetricsValue([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _healthMetricValueService.GetAllHealthMetricsValueByUserIdAsync(
                requestListWithPeriodByIdDTO.UserId,
                requestListWithPeriodByIdDTO.BegDate,
                requestListWithPeriodByIdDTO.EndDate);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(_mapper.Map<List<HealthMetricValueDTO>>(result));
        }

        /// <summary>
        /// Получить значение показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueId">Идентификатор значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpGet(nameof(GetHealthMetricValueById))]
        [Authorize]
        public async Task<IActionResult> GetHealthMetricValueById(int healthMetricValueId)
        {
            var result = await _healthMetricValueService.GetHealthMetricValueByIdAsync(healthMetricValueId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HealthMetricValueDTO>(result));
        }
    }
}
