using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Intake;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данные о приеме лекарств пользователем
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class IntakeController(IIntakeService intakeService) : Controller
    {
        readonly IIntakeService _intakeService = intakeService;

        /// <summary>
        /// Зарегистрировать прием лекарств
        /// </summary>
        /// <param name="intakeDTO">Данные для регисрации о приеме лекарств</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateIntake))]
        [Authorize]
        public async Task<IActionResult> CreateIntake([FromBody] IntakeCreateDTO intakeDTO)
        {
            await _intakeService.CreateIntakeAsync(intakeDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные примема лекарств
        /// </summary>
        /// <param name="intakeUpdateDTO">Измененные данные приема лекарств</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateIntake))]
        [Authorize]
        public async Task<IActionResult> UpdateIntake([FromBody] IntakeUpdateDTO intakeUpdateDTO)
        {
            await _intakeService.UpdateIntakeAsync(intakeUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить данные о приеме лекарств
        /// </summary>
        /// <param name="intakeId">Идентификатор приема лекарств</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteIntake))]
        [Authorize]
        public async Task<IActionResult> DeleteIntake(int intakeId)
        {
            await _intakeService.DeleteIntakeAsync(intakeId);
            return Ok();
        }

        /// <summary>
        /// Получить все приемы лекарств по пользователю за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllIntakes))]
        public async Task<IActionResult> GetAllIntakes([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _intakeService.GetAllIntakeByUserIdAsync(requestListWithPeriodByIdDTO);

            return Ok(result);
        }

        /// <summary>
        /// Получить данные приема лекарств
        /// </summary>
        /// <param name="intakeId">Идентификатор данных приема лекарств</param>
        /// <returns></returns>
        [HttpGet(nameof(GetIntakeById))]
        public async Task<IActionResult> GetIntakeById(int intakeId)
        {
            var result = await _intakeService.GetIntakeByIdAsync(intakeId);           

            return Ok(result);
        }
    }
}
