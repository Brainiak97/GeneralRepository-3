using AutoMapper;
using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.Reminder;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Reminder;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestListWithPeriodByIdDTO = MetricService.BLL.DTO.RequestListWithPeriodByIdDTO;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с напоминаниями о приеме лекарств.
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReminderController(IReminderService reminderService, IMapper mapper) : Controller
    {
        private readonly IReminderService _reminderService = reminderService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// зарегистрировать напоминание о приеме лекарств
        /// </summary>
        /// <param name="apiReminderCreateRequestDTO">Данные для регистрации напоминания о приеме лекарств</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateReminder))]
        public async Task<IActionResult> CreateReminder([FromBody] ApiReminderCreateRequestDTO apiReminderCreateRequestDTO)
        {
            var reminderCreateRequestDTO = _mapper.Map<ReminderCreateDTO>(apiReminderCreateRequestDTO);
            await _reminderService.CreateReminderAsync(reminderCreateRequestDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные напоминаяния о приеме лекарств
        /// </summary>
        /// <param name="apiReminderUpdateRequestDTO">Измененные данные для напоминания оприеме лекарств</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateReminder))]
        public async Task<IActionResult> UpdateReminder([FromBody] ApiReminderUpdateRequestDTO apiReminderUpdateRequestDTO)
        {
            var reminderUpdateRequestDTO = _mapper.Map<ReminderUpdateDTO>(apiReminderUpdateRequestDTO);
            await _reminderService.UpdateReminderAsync(reminderUpdateRequestDTO);
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
        /// <param name="apiListWithPeriodByIdRequestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllRemindersByUser))]
        public async Task<IActionResult> GetAllRemindersByUser([FromQuery] ApiListWithPeriodByIdRequestDTO apiListWithPeriodByIdRequestDTO)
        {
            var requestListWithPeriodByIdDTO = _mapper.Map<RequestListWithPeriodByIdDTO>(apiListWithPeriodByIdRequestDTO);

            var reminders = await _reminderService.GetAllReminderByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!reminders.Any())
            {
                return Ok("Список пуст");
            }

            var result = _mapper.Map<List<ApiReminderDTO>>(reminders);

            return Ok(result);
        }

        /// <summary>
        /// Получить список напоминаний по схеме приема медикаментов за период
        /// </summary>
        /// <param name="apiRequestListWithPeriodByRegimenIdDTO">Данные о схеме приема медикаментов и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllRemindersByRegimen))]
        public async Task<IActionResult> GetAllRemindersByRegimen([FromQuery] ApiRequestListWithPeriodByRegimenIdDTO apiRequestListWithPeriodByRegimenIdDTO)
        {
            var requestListWithPeriodByRegimenIdDTO = _mapper.Map<RequestListWithPeriodByRegimenIdDTO>(apiRequestListWithPeriodByRegimenIdDTO);

            var reminders = await _reminderService.GetAllReminderByRegimenIdAsync(requestListWithPeriodByRegimenIdDTO);

            if (!reminders.Any())
            {
                return Ok("Список пуст");
            }

            var result = _mapper.Map<List<ApiReminderDTO>>(reminders);

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
            var reminder = await _reminderService.GetReminderByIdAsync(reminderid);

            var result = _mapper.Map<ApiReminderDTO>(reminder);

            return Ok(result);
        }

        /// <summary>
        /// Доставить напоминаяния пользователю
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(ReminderDelivery))]
        public async Task<IActionResult> ReminderDelivery(int userId)
        {
            var reminrers = await _reminderService.ReminderDeliveryAsync(userId);

            if (!reminrers.Any())
            {
                return Ok("Список пуст");
            }

            var result = _mapper.Map<List<ApiReminderDTO>>(reminrers);

            return Ok(result);
        }
    }
}
