using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisTypeController(IAnalysisTypeService analysisTypeService) : Controller
    {

        readonly IAnalysisTypeService _analysisTypeService = analysisTypeService;

        [HttpPost(nameof(CreateAnalysisType))]
        [Authorize]
        public async Task<IActionResult> CreateAnalysisType([FromBody] AnalysisTypeCreateDTO analysisTypeCreateDTO)
        {
            await _analysisTypeService.CreateAnalysisTypeAsync(analysisTypeCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateAnalysisType))]
        [Authorize]
        public async Task<IActionResult> UpdateAnalysisType([FromBody] AnalysisTypeUpdateDTO analysisTypeUpdateDTO)
        {
            await _analysisTypeService.UpdateAnalysisTypeAsync(analysisTypeUpdateDTO);
            return Ok();
        }


        [HttpDelete(nameof(DeleteAnalysisType))]
        [Authorize]
        public async Task<IActionResult> DeleteAnalysisType(int id)
        {
            await _analysisTypeService.DeleteAnalysisTypeAsync(id);
            return Ok();
        }


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


        [HttpGet(nameof(GetAnalysisTypeById))]
        public async Task<IActionResult> GetAnalysisTypeById(int categoryid)
        {
            var result = await _analysisTypeService.GetAnalysisTypeByIdAsync(categoryid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


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
