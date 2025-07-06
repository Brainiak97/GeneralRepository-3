using MetricService.BLL.DTO.MedicationDTO;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы со справочником "Медикаменты"
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController(IMedicationService medicationService) : Controller
    {

        readonly IMedicationService _medicationService = medicationService;

        /// <summary>
        /// Зарегистрировать медикамент в справочнике "Медикаменты"
        /// </summary>
        /// <param name="medicationCreateDTO">Данные для регистрации медикамента в справочнике</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateMedication))]
        [Authorize]
        public async Task<IActionResult> CreateMedication([FromBody] MedicationCreateDTO medicationCreateDTO)
        {
            await _medicationService.CreateMedicationAsync(medicationCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные регистрации медикамента в справочнике "Медикаменты"
        /// </summary>
        /// <param name="medicationUpdateDTO">Измененные данные медикамента</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateMedication))]
        [Authorize]
        public async Task<IActionResult> UpdateMedication([FromBody] MedicationUpdateDTO medicationUpdateDTO)
        {
            await _medicationService.UpdateMedicationAsync(medicationUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить медикамент из справочника "Медикаменты"
        /// </summary>
        /// <param name="medicationid">Идентификатор данных медикамента</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteMedication))]
        [Authorize]
        public async Task<IActionResult> DeleteMedication(int medicationid)
        {
            await _medicationService.DeleteMedicationAsync(medicationid);
            return Ok();
        }

        /// <summary>
        /// Получить список медикаментов из справочника "Медикаменты"
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllMedications))]
        public async Task<IActionResult> GetAllMedications()
        {
            var result = await _medicationService.GetAllMedicationAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить медикамент из справочника
        /// </summary>
        /// <param name="medicationid">Идентификатор данных медикамента</param>
        /// <returns></returns>
        [HttpGet(nameof(GetMedicationById))]
        public async Task<IActionResult> GetMedicationById(int medicationid)
        {
            var result = await _medicationService.GetMedicationByIdAsync(medicationid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
