using AutoMapper;
using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.HealthCondition;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthCondition;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestListWithPeriodByIdDTO = MetricService.BLL.DTO.RequestListWithPeriodByIdDTO;

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
        /// <param name="apiHealtConditionCreateRequest">Данные для регистрации нового значения самочувствия пользователя</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateHealthCondition))]
        [Authorize]
        public async Task<IActionResult> CreateHealthCondition([FromBody] ApiHealtConditionCreateRequest apiHealtConditionCreateRequest)
        {
            var healthConditionCreateDTO = _mapper.Map<HealthConditionCreateDTO>(apiHealtConditionCreateRequest);
            await _healthConditionService.CreateHealthConditionAsync(healthConditionCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о самочувствии(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="apiHealthConditionUpdateRequestDTO">Данные для изменения значения самочувствия(состояния здоровья) пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateHealthCondition))]
        [Authorize]
        public async Task<IActionResult> UpdateHealthCondition([FromBody] ApiHealthConditionUpdateRequestDTO apiHealthConditionUpdateRequestDTO)
        {
            var healthConditionUpdateDTO = _mapper.Map<HealthConditionUpdateDTO>(apiHealthConditionUpdateRequestDTO);
            await _healthConditionService.UpdateHealthConditionAsync(healthConditionUpdateDTO);
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
            await _healthConditionService.DeleteHealthConditionAsync(healthConditionId);
            return Ok();
        }

        /// <summary>
        /// Получить список значений самочувствия пользователя
        /// </summary>
        /// <param name="apiListWithPeriodByIdRequestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllHealthConditions))]
        [Authorize]
        public async Task<IActionResult> GetAllHealthConditions([FromQuery] ApiListWithPeriodByIdRequestDTO apiListWithPeriodByIdRequestDTO)
        {
            var requestListWithPeriodByIdDTO = _mapper.Map<RequestListWithPeriodByIdDTO>(apiListWithPeriodByIdRequestDTO);
            var healthConditions = await _healthConditionService.GetAllHealthConditionsByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!healthConditions.Any())
            {
                return Ok("Список пуст");
            }

            var result = _mapper.Map<List<ApiHealthConditionDTO>>(healthConditions);

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
            var healthCondition = await _healthConditionService.GetHealthConditionByIdAsync(healthConditionId);                       

            var result = _mapper.Map<ApiHealthConditionDTO>(healthCondition);

            return Ok(result);
        }
    }
}
