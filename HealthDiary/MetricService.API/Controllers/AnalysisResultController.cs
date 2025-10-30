using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данными результатов анализов пользователя
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AnalysisResultController(IAnalysisResultService analysisResultService) : Controller
    {
        private readonly IAnalysisResultService _analysisResultService = analysisResultService;

        /// <summary>
        /// Зарегистрировать данные анализа пользователя
        /// </summary>
        /// <param name="analysisResultCreateDTO">Данные анализа пользователя для регистрации</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateAnalysisResult))]
        public async Task<IActionResult> CreateAnalysisResult([FromBody] AnalysisResultCreateDTO analysisResultCreateDTO)
        {
            await _analysisResultService.CreateAnalysisResultAsync(analysisResultCreateDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные анализа пользователя
        /// </summary>
        /// <param name="analysisResultUpdateDTO">Измененные данные анализа пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateAnalysisResult))]
        public async Task<IActionResult> UpdateAnalysisResult([FromBody] AnalysisResultUpdateDTO analysisResultUpdateDTO)
        {
            await _analysisResultService.UpdateAnalysisResultAsync(analysisResultUpdateDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить данные анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">Идентификатор данных анализа пользователя</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteAnalysisResult))]
        public async Task<IActionResult> DeleteAnalysisResult(int analysisResultId)
        {
            await _analysisResultService.DeleteAnalysisResultAsync(analysisResultId);
            return Ok();
        }

        /// <summary>
        /// Получить список анализов пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllAnalysisResults))]
        public async Task<IActionResult> GetAllAnalysisResults([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _analysisResultService.GetAllAnalysisResultsByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить данные анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">Идентификатор данные анализа пользователя</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAnalysisResultById))]
        public async Task<IActionResult> GetAnalysisResultById(int analysisResultId)
        {
            return Ok(await _analysisResultService.GetAnalysisResultByIdAsync(analysisResultId));
        }
    }
}
