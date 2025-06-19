using MetricService.BLL.DTO.Workout;
using MetricService.BLL.DTO;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetricService.BLL.DTO.Reminder;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReminderController(IReminderService reminderService) : Controller
    {
        private readonly IReminderService _reminderService = reminderService;

        [HttpPost(nameof(CreateReminder))]
        public async Task<IActionResult> CreateReminder([FromBody] ReminderCreateDTO reminderCreateDTO)
        {
            await _reminderService.CreateReminderAsync(reminderCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateReminder))]
        public async Task<IActionResult> UpdateReminder([FromBody] ReminderUpdateDTO reminderUpdateDTO)
        {
            await _reminderService.UpdateReminderAsync(reminderUpdateDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteReminderAsync))]
        public async Task<IActionResult> DeleteReminderAsync(int id)
        {
            await _reminderService.DeleteReminderAsync(id);
            return Ok();
        }

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

        [HttpGet(nameof(GetReminderById))]
        public async Task<IActionResult> GetReminderById(int reminderid)
        {
            return Ok(await _reminderService.GetReminderByIdAsync(reminderid));
        }
    }
}
