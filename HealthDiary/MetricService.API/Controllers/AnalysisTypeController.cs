using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.DTO.PhysicalActivity;
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
    public class AnalysisTypeController(IAnalysisTypeService analysisTypeService) : Controller
    {

        readonly IAnalysisTypeService _analysisTypeService = analysisTypeService;

        [HttpPost(nameof(CreateAnalysisType))]
        [Authorize]
        public async Task<IActionResult> CreateAnalysisType([FromBody] AnalysisTypeCreateDTO analysisTypeCreateDTO)
        {
            try
            {
                await _analysisTypeService.CreateAnalysisTypeAsync(analysisTypeCreateDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpPost(nameof(UpdateAnalysisType))]
        [Authorize]
        public async Task<IActionResult> UpdateAnalysisType([FromBody] AnalysisTypeUpdateDTO analysisTypeUpdateDTO)
        {
            try
            {
                await _analysisTypeService.UpdateAnalysisTypeAsync(analysisTypeUpdateDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpDelete(nameof(DeleteAnalysisType))]
        [Authorize]
        public async Task<IActionResult> DeleteAnalysisType(int id)
        {
            try
            {
                await _analysisTypeService.DeleteAnalysisTypeAsync(id);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpGet(nameof(GetAllAnalysisTypes))]
        public async Task<IActionResult> GetAllAnalysisTypes(int pagenum, int pagesize)
        {
            var result = await _analysisTypeService.GetAllAnalysisTypeAsync(pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }


        [HttpGet(nameof(GetAnalysisTypeById))]
        public async Task<IActionResult> GetAnalysisTypeById(int categoryid)
        {
            try
            {
                var result = await _analysisTypeService.GetAnalysisTypeByIdAsync(categoryid);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
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
