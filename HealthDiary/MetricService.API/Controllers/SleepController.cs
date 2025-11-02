using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данными о сне пользователя
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SleepController(ISleepService sleepService) : Controller
    {
        private readonly ISleepService _sleepService = sleepService;

        /// <summary>
        /// Зарегистрировать данные о сне пользователя
        /// </summary>
        /// <param name="sleepDTO">Данные о сне пользователя</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateSleep))]
        public async Task<IActionResult> CreateSleep([FromBody] SleepCreateDTO sleepDTO)
        {
            await _sleepService.CreateRecordOfSleepAsync(sleepDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о сне пользователя
        /// </summary>
        /// <param name="sleepDTO">Измененные данные о сне пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateSleep))]
        public async Task<IActionResult> UpdateSleep([FromBody] SleepUpdateDTO sleepDTO)
        {
            await _sleepService.UpdateRecordOfSleepAsync(sleepDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить данные о сне пользователя
        /// </summary>
        /// <param name="sleepId">идентификатор сна пользователя</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteSleep))]
        public async Task<IActionResult> DeleteSleep(int sleepId)
        {
            await _sleepService.DeleteRecordOfSleepAsync(sleepId);
            return Ok();
        }

        /// <summary>
        /// Получить список данных о снах пользователя за период
        /// </summary>
        /// <param name="request">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllSleeps))]
        public async Task<IActionResult> GetAllSleeps([FromQuery] RequestListWithPeriodByIdDTO request)
        {
            var result = await _sleepService.GetAllRecordsOfSleepByUserIdAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Получить данные о сне пользователя
        /// </summary>
        /// <param name="sleepId">Идентификатор данных сна пользователя</param>
        /// <returns></returns>
        [HttpGet(nameof(GetSleepById))]
        public async Task<IActionResult> GetSleepById(int sleepId)
        {
            return Ok(await _sleepService.GetRecordOfSleepByIdAsync(sleepId));
        }
    }
}
