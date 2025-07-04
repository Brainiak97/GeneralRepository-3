using MetricService.BLL.DTO;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetricService.BLL.DTO.Regimen;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы со схемами приема лекарств
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegimenController(IRegimenService regimenService) : Controller
    {
        private readonly IRegimenService _regimenService = regimenService;

        /// <summary>
        /// Зарегистрировать схему приема лекарств
        /// </summary>
        /// <param name="regimenCreateDTO">Данные для регистрации схемы приема лекарств</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateRegimen))]
        public async Task<IActionResult> CreateRegimen([FromBody] RegimenCreateDTO regimenCreateDTO)
        {
            await _regimenService.CreateRegimenAsync(regimenCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные схемы приема лекарств
        /// </summary>
        /// <param name="regimenUpdateDTO">Данные для изменения схемы приема лекарств</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateRegimen))]
        public async Task<IActionResult> UpdateRegimen([FromBody] RegimenUpdateDTO regimenUpdateDTO)
        {
            await _regimenService.UpdateRegimenAsync(regimenUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить схему приема лекарств
        /// </summary>
        /// <param name="regimenId">Идентификатор схемы</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteRegimenAsync))]
        public async Task<IActionResult> DeleteRegimenAsync(int regimenId)
        {
            await _regimenService.DeleteRegimenAsync(regimenId);
            return Ok();
        }

        /// <summary>
        /// Получить все схемы приема лекарств по пользователю за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllRegimens))]
        public async Task<IActionResult> GetAllRegimens([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _regimenService.GetAllRegimenByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить схему приема лекарств
        /// </summary>
        /// <param name="regimenid">Идентификатор схемы приема лекарств</param>
        /// <returns></returns>
        [HttpGet(nameof(GetRegimenById))]
        public async Task<IActionResult> GetRegimenById(int regimenid)
        {
            return Ok(await _regimenService.GetRegimenByIdAsync(regimenid));
        }
    }
}
