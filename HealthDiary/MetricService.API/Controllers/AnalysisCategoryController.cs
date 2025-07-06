using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данными справочника "Категории анализов"
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisCategoryController(IAnalysisCategoryService analysisCategoryService) : Controller
    {

        readonly IAnalysisCategoryService _analysisCategoryService = analysisCategoryService;

        /// <summary>
        /// Зарегистрировать новую категорию анализов в справочнике "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryCreateDTO">Данные для регистрации новой категории анализов</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> CreateAnalysisCategory([FromBody] AnalysisCategoryCreateDTO analysisCategoryCreateDTO)
        {
            await _analysisCategoryService.CreateAnalysisCategoryAsync(analysisCategoryCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные категории анализов в справочнике "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryUpdateDTO">Данные для изменения категории анализов</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> UpdateAnalysisCategory([FromBody] AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO)
        {
            await _analysisCategoryService.UpdateAnalysisCategoryAsync(analysisCategoryUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить категорию анализов из справочника "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryId">Идентификатор категории анализов</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> DeleteAnalysisCategory(int analysisCategoryId)
        {
            await _analysisCategoryService.DeleteAnalysisCategoryAsync(analysisCategoryId);
            return Ok();
        }

        /// <summary>
        /// Получить список категорий анализов из справочника "Категории анализов"
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllAnalysisCategories))]
        public async Task<IActionResult> GetAllAnalysisCategories()
        {
            var result = await _analysisCategoryService.GetAllAnalysisCategoriesAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить категорию анализов из справочника "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryId">Идентификатор категории анализов</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAnalysisCategoryById))]
        public async Task<IActionResult> GetAnalysisCategoryById(int analysisCategoryId)
        {
            var result = await _analysisCategoryService.GetAnalysisCategoryByIdAsync(analysisCategoryId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Поучить из справочника "Категории анализов" все подходящие строки, заданные критерием
        /// </summary>
        /// <param name="search">Строка поиска. Для множественного поиска фразы разделять запятой</param>
        /// <returns></returns>
        [HttpGet(nameof(FindAnalysisCategoryByName))]
        public async Task<IActionResult> FindAnalysisCategoryByName(string search)
        {
            var result = await _analysisCategoryService.GetListAnalysisCategoriesBySearchAsync(search);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }        
    }
}
