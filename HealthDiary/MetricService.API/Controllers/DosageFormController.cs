using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данными справочника "Формы выпуска препарата"
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class DosageFormController(IDosageFormService dosageFormService) : Controller
    {
        readonly IDosageFormService _dosageFormService = dosageFormService;

        /// <summary>
        /// Зарегистрировать новую форму препарата в справочнике "Формы выпуска препарата"
        /// </summary>
        /// <param name="dosageFormCreateDTO">Данные для регистрации формы препарата</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateDosageForm))]
        [Authorize]
        public async Task<IActionResult> CreateDosageForm([FromBody] DosageFormCreateDTO dosageFormCreateDTO)
        {
            await _dosageFormService.CreateDosageFormAsync(dosageFormCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о форме препарата в справочнике "Формы выпуска препарата"
        /// </summary>
        /// <param name="dosageFormUpdateDTO">Данные для изменения формы препарата</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateDosageForm))]
        [Authorize]
        public async Task<IActionResult> UpdateDosageForm([FromBody] DosageFormUpdateDTO dosageFormUpdateDTO)
        {
            await _dosageFormService.UpdateDosageFormAsync(dosageFormUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить данные формы препарата из справочника "Формы выпуска препарата"
        /// </summary>
        /// <param name="dosageFormId">Идентификатор формы препарата</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteDosageForm))]
        [Authorize]
        public async Task<IActionResult> DeleteDosageForm(int dosageFormId)
        {
            await _dosageFormService.DeleteDosageFormAsync(dosageFormId);
            return Ok();
        }

        /// <summary>
        /// Получить список форм препаратов из справочника "Формы выпуска препарата"
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllDosageForms))]
        public async Task<IActionResult> GetAllDosageForms()
        {
            var result = await _dosageFormService.GetAllDosageFormsAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить форму препарата из справочника "Формы выпуска препарата"
        /// </summary>
        /// <param name="dosageFormId">Идентификатор формы препарата</param>
        /// <returns></returns>
        [HttpGet(nameof(GetDosageFormById))]
        public async Task<IActionResult> GetDosageFormById(int dosageFormId)
        {
            var result = await _dosageFormService.GetDosageFormByIdAsync(dosageFormId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
