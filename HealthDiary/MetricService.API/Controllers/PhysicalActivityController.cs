using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы со справочником физических активностей
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalActivityController(IPhysicalActivityService physicalActivityService) : Controller
    {
        readonly IPhysicalActivityService _physicalActivityService = physicalActivityService;

        /// <summary>
        /// Зарегисрировать новую физичекую активность в справочнике
        /// </summary>
        /// <param name="physicalActivityCreateDTO">Данные для физической активности</param>
        /// <returns></returns>
        [HttpPost(nameof(CreatePhysicalActivity))]
        [Authorize]
        public async Task<IActionResult> CreatePhysicalActivity([FromBody] PhysicalActivityCreateDTO physicalActivityCreateDTO)
        {
            await _physicalActivityService.CreatePhysicalActivityAsync(physicalActivityCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные физической активности в справочнике
        /// </summary>
        /// <param name="physicalActivityUpdateDTO">Измененные данные физической активности</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdatePhysicalActivity))]
        [Authorize]
        public async Task<IActionResult> UpdatePhysicalActivity([FromBody] PhysicalActivityUpdateDTO physicalActivityUpdateDTO)
        {
            await _physicalActivityService.UpdatePhysicalActivityAsync(physicalActivityUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить физическую активность из справочника
        /// </summary>
        /// <param name="physicalActivityId">Идентификатор физической активности</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeletePhysicalActivity))]
        [Authorize]
        public async Task<IActionResult> DeletePhysicalActivity(int physicalActivityId)
        {
            await _physicalActivityService.DeletePhysicalActivityAsync(physicalActivityId);
            return Ok();
        }

        /// <summary>
        /// Получить список физических активностей из справочника
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllPhysicalActivities))]
        public async Task<IActionResult> GetAllPhysicalActivities()
        {
            var result = await _physicalActivityService.GetAllPhysicalActivitiesAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Поучить физическую активность из справочника
        /// </summary>
        /// <param name="physicalActivityId">Идентификатор физической активности</param>
        /// <returns></returns>
        [HttpGet(nameof(GetPhysicalActivityById))]
        public async Task<IActionResult> GetPhysicalActivityById(int physicalActivityId)
        {
            var result = await _physicalActivityService.GetPhysicalActivityByIdAsync(physicalActivityId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить все подходящие физические активности по строке поиска
        /// </summary>
        /// <param name="search">Строка поиска. Для множественного поиска, фразы в строке разделяйте запятой</param>
        /// <returns></returns>
        [HttpGet(nameof(FindPhysicalActivityByName))]
        public async Task<IActionResult> FindPhysicalActivityByName(string search)
        {
            var result = await _physicalActivityService.GetListPhysicalActivitiesBySearchAsync(search);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
