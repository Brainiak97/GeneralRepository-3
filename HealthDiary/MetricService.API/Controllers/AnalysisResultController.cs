using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AnalysisResultController(IAnalysisResultService analysisResultService) : Controller
    {
        private readonly IAnalysisResultService _analysisResultService = analysisResultService;



        [HttpPost(nameof(CreateAnalysisResult))]
        public async Task<IActionResult> CreateAnalysisResult([FromBody] AnalysisResultCreateDTO analysisResultCreateDTO)
        {
            await _analysisResultService.CreateAnalysisResultAsync(analysisResultCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateAnalysisResult))]
        public async Task<IActionResult> UpdateAnalysisResult([FromBody] AnalysisResultUpdateDTO analysisResultUpdateDTO)
        {
            await _analysisResultService.UpdateAnalysisResultAsync(analysisResultUpdateDTO);
            return Ok();
        }


        [HttpDelete(nameof(DeleteAnalysisResult))]
        public async Task<IActionResult> DeleteAnalysisResult(int id)
        {
            await _analysisResultService.DeleteAnalysisResultAsync(id);
            return Ok();
        }

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


        [HttpGet(nameof(GetAnalysisResultById))]
        public async Task<IActionResult> GetAnalysisResultById(int analysisResultId)
        {
            return Ok(await _analysisResultService.GetAnalysisResultByIdAsync(analysisResultId));
        }
    }
}
