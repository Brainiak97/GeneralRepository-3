using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.DTO.HealthMetric;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Services;
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
    public class HealthMetric(IHealthMetricService healthMetricService) : Controller
    {
        readonly IHealthMetricService _healthMetricService = healthMetricService;

        /// <summary>
        /// Зарегистрировать новый показатель здоровья пользователя
        /// </summary>
        /// <param name="healthMetricCreateDTO">Данные для регистрации нового показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateHealthMetric))]
        [Authorize]
        public async Task<IActionResult> CreateHealthMetric([FromBody] HealthMetricCreateDTO healthMetricCreateDTO)
        {
            await _healthMetricService.CreateHealthMetricAsync(healthMetricCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о показателе здоровья пользователя
        /// </summary>
        /// <param name="healthMetricUpdateDTO">Данные для изменения показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateHealthMetric))]
        [Authorize]
        public async Task<IActionResult> UpdateHealthMetric([FromBody] HealthMetricUpdateDTO healthMetricUpdateDTO)
        {
            await _healthMetricService.UpdateHealthMetricAsync(healthMetricUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить данные о показателе здоровья пользователя
        /// </summary>
        /// <param name="healthMetricId">Идентификатор показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteHealthMetric))]
        [Authorize]
        public async Task<IActionResult> DeleteHealthMetric(int healthMetricId)
        {
            await _healthMetricService.DeleteHealthMetricAsync(healthMetricId);
            return Ok();
        }

        /// <summary>
        /// Получить список показателей здоровья пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllHealthMetrics))]
        [Authorize]
        public async Task<IActionResult> GetAllHealthMetrics()
        {
            var result = await _healthMetricService.GetAllHealthMetricsAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить показатель здоровья пользователя
        /// </summary>
        /// <param name="healthMetricId">Идентификатор показателя здоровья пользователя</param>
        /// <returns></returns>
        [HttpGet(nameof(GetHealthMetricById))]
        [Authorize]
        public async Task<IActionResult> GetHealthMetricById(int healthMetricId)
        {
            var result = await _healthMetricService.GetHealthMetricByIdAsync(healthMetricId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
