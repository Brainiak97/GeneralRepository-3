using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Services;
using MetricService.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AnalysisResultController(IAnalysisResultService analysisResultService) : Controller
    {
        private readonly IAnalysisResultService _analysisResultService = analysisResultService;



        [HttpPost(nameof(CreateAnalysisResult))]
        public async Task<IActionResult> CreateAnalysisResult([FromBody] AnalysisResultCreateDTO analysisResultCreateDTO)
        {
            try
            {
                await _analysisResultService.CreateAnalysisResultAsync(analysisResultCreateDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpPost(nameof(UpdateAnalysisResult))]
        public async Task<IActionResult> UpdateAnalysisResult([FromBody] AnalysisResultUpdateDTO analysisResultUpdateDTO)
        {
            try
            {
                await _analysisResultService.UpdateAnalysisResultAsync(analysisResultUpdateDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpDelete(nameof(DeleteAnalysisResult))]
        public async Task<IActionResult> DeleteAnalysisResult(int id)
        {
            try
            {
                await _analysisResultService.DeleteAnalysisResultAsync(id);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
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
            try
            {
                return Ok(await _analysisResultService.GetAnalysisResultByIdAsync(analysisResultId));
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }
    }
}
