using MetricService.BLL.DTO;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetricService.BLL.DTO.Reminder;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с напоминаниями о приеме лекарств.
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReminderController(IReminderService reminderService) : Controller
    {
        private readonly IReminderService _reminderService = reminderService;

        /// <summary>
        /// зарегистрировать напоминание о приеме лекарств
        /// </summary>
        /// <param name="reminderCreateDTO">Данные для регистрации напоминания о приеме лекарств</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateReminder))]
        public async Task<IActionResult> CreateReminder([FromBody] ReminderCreateDTO reminderCreateDTO)
        {
            await _reminderService.CreateReminderAsync(reminderCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные напоминаяния о приеме лекарств
        /// </summary>
        /// <param name="reminderUpdateDTO">Измененные данные для напоминания оприеме лекарств</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateReminder))]
        public async Task<IActionResult> UpdateReminder([FromBody] ReminderUpdateDTO reminderUpdateDTO)
        {
            await _reminderService.UpdateReminderAsync(reminderUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить напоминание о приеме лекарств
        /// </summary>
        /// <param name="id">Идентификатор напоминания о приеме лекарств</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteReminderAsync))]
        public async Task<IActionResult> DeleteReminderAsync(int id)
        {
            await _reminderService.DeleteReminderAsync(id);
            return Ok();
        }

        /// <summary>
        /// Получить список напоминаний по пользователю за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllRemindersByUser))]
        public async Task<IActionResult> GetAllRemindersByUser([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _reminderService.GetAllReminderByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить список напоминаний по схеме приема медикаментов за период
        /// </summary>
        /// <param name="requestListWithPeriodByRegimenIdDTO">Данные о схеме приема медикаментов и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllRemindersByRegimen))]
        public async Task<IActionResult> GetAllRemindersByRegimen([FromQuery] RequestListWithPeriodByRegimenIdDTO requestListWithPeriodByRegimenIdDTO)
        {
            var result = await _reminderService.GetAllReminderByRegimenIdAsync(requestListWithPeriodByRegimenIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить напоминание о приеме лекарств
        /// </summary>
        /// <param name="reminderid">Идентификатор напоминания</param>
        /// <returns></returns>
        [HttpGet(nameof(GetReminderById))]
        public async Task<IActionResult> GetReminderById(int reminderid)
        {
            return Ok(await _reminderService.GetReminderByIdAsync(reminderid));
        }
    }
}
