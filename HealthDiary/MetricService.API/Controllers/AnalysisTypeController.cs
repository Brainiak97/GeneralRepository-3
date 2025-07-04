using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данными справочника типов анализов
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisTypeController(IAnalysisTypeService analysisTypeService) : Controller
    {

        readonly IAnalysisTypeService _analysisTypeService = analysisTypeService;

        /// <summary>
        /// Зарегистрировать новый тип анализов в справочнике
        /// </summary>
        /// <param name="analysisTypeCreateDTO">Данные о новом типе анализов для регистрации</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateAnalysisType))]
        [Authorize]
        public async Task<IActionResult> CreateAnalysisType([FromBody] AnalysisTypeCreateDTO analysisTypeCreateDTO)
        {
            await _analysisTypeService.CreateAnalysisTypeAsync(analysisTypeCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о типе анализов в справочнике
        /// </summary>
        /// <param name="analysisTypeUpdateDTO">Измененные данные о типе анализов</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateAnalysisType))]
        [Authorize]
        public async Task<IActionResult> UpdateAnalysisType([FromBody] AnalysisTypeUpdateDTO analysisTypeUpdateDTO)
        {
            await _analysisTypeService.UpdateAnalysisTypeAsync(analysisTypeUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить данные о типе анализов из справочника
        /// </summary>
        /// <param name="analysisTypeId">Идентификатор типа анализов</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteAnalysisType))]
        [Authorize]
        public async Task<IActionResult> DeleteAnalysisType(int analysisTypeId)
        {
            await _analysisTypeService.DeleteAnalysisTypeAsync(analysisTypeId);
            return Ok();
        }

        /// <summary>
        /// Получить список типов анализов из справочника
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllAnalysisTypes))]
        public async Task<IActionResult> GetAllAnalysisTypes()
        {
            var result = await _analysisTypeService.GetAllAnalysisTypeAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }

        /// <summary>
        /// Получить тип анализа из справочника 
        /// </summary>
        /// <param name="analysisTypeId">Идентификатор типа анализа</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAnalysisTypeById))]
        public async Task<IActionResult> GetAnalysisTypeById(int analysisTypeId)
        {
            var result = await _analysisTypeService.GetAnalysisTypeByIdAsync(analysisTypeId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Поучить из справочника типов анализов все подходящие строки, заданные критерием
        /// </summary>
        /// <param name="search">Строка поиска. Для множественного поиска фразы разделять запятой</param>
        /// <returns></returns>
        [HttpGet(nameof(FindAnalysisTypeByName))]
        public async Task<IActionResult> FindAnalysisTypeByName(string search)
        {
            var result = await _analysisTypeService.GetListAnalysisTypeBySearchAsync(search);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
