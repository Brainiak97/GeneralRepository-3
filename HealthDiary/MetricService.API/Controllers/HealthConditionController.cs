using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthCondition;
using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с показателями самочувствия(состояния здоровья) пользователя
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class HealthConditionController(IHealthConditionService healthConditionService, IMapper mapper) : Controller
    {
        private readonly IHealthConditionService _healthConditionService = healthConditionService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Зарегистрировать новое значение самочувствия пользователя
        /// </summary>
        /// <param name="healthConditionCreateDTO">Данные для регистрации нового значения самочувствия пользователя</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateHealthCondition))]
        [Authorize]
        public async Task<IActionResult> CreateHealthCondition([FromBody] HealthConditionCreateDTO healthConditionCreateDTO)
        {
            var healthCondition = _mapper.Map<HealthCondition>(healthConditionCreateDTO);
            await _healthConditionService.CreateRecordOfHealthCondAsync(healthCondition);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о самочувствии(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="healthConditionUpdateDTO">Данные для изменения значения самочувствия(состояния здоровья) пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateHealthCondition))]
        [Authorize]
        public async Task<IActionResult> UpdateHealthCondition([FromBody] HealthConditionUpdateDTO healthConditionUpdateDTO)
        {
            var healthCondition = _mapper.Map<HealthCondition>(healthConditionUpdateDTO);
            await _healthConditionService.UpdateRecordOfHealthCondAsync(healthCondition);
            return Ok();
        }

        /// <summary>
        /// Удалить данные о самочувствия(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="healthConditionId">Идентификатор значения самочувствия пользователя</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteHealthCondition))]
        [Authorize]
        public async Task<IActionResult> DeleteHealthCondition(int healthConditionId)
        {
            await _healthConditionService.DeleteRecordOfHealthCondAsync(healthConditionId);
            return Ok();
        }

        /// <summary>
        /// Получить список значений самочувствия пользователя
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllHealthCondition))]
        [Authorize]
        public async Task<IActionResult> GetAllHealthCondition([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var healthConditions = await _healthConditionService.GetAllRecordsOfHealthCondByUserIdAsync(
                requestListWithPeriodByIdDTO.UserId,
                requestListWithPeriodByIdDTO.BegDate,
                requestListWithPeriodByIdDTO.EndDate);

            if (!healthConditions.Any())
            {
                return Ok("Список пуст");
            }

            var result = _mapper.Map<List<HealthConditionDTO>>(healthConditions);

            return Ok(result);
        }

        /// <summary>
        /// Получить значение самочувствия пользователя
        /// </summary>
        /// <param name="healthConditionId">Идентификатор значения самочувствия пользователя</param>
        /// <returns></returns>
        [HttpGet(nameof(GetHealthConditionById))]
        [Authorize]
        public async Task<IActionResult> GetHealthConditionById(int healthConditionId)
        {
            var healthCondition = await _healthConditionService.GetRecordOfHealthCondByIdAsync(healthConditionId);

            if (healthCondition == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<HealthConditionDTO>(healthCondition);

            return Ok(result);
        }
    }
}
