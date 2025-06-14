using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AnalysisCategoryController(IAnalysisCategoryService analysisCategoryService) : Controller
    {

        readonly IAnalysisCategoryService _analysisCategoryService = analysisCategoryService;

        [HttpPost(nameof(CreateAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> CreateAnalysisCategory([FromBody] AnalysisCategoryCreateDTO analysisCategoryCreateDTO)
        {
            try
            {
                await _analysisCategoryService.CreateAnalysisCategoryAsync(analysisCategoryCreateDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpPost(nameof(UpdateAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> UpdateAnalysisCategory([FromBody] AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO)
        {
            try
            {
                await _analysisCategoryService.UpdateAnalysisCategoryAsync(analysisCategoryUpdateDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpDelete(nameof(DeleteAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> DeleteAnalysisCategory(int id)
        {
            try
            {
                await _analysisCategoryService.DeleteAnalysisCategoryAsync(id);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }


        [HttpGet(nameof(GetAllAnalysisCategories))]
        public async Task<IActionResult> GetAllAnalysisCategories(int pagenum, int pagesize)
        {
            var result = await _analysisCategoryService.GetAllAnalysisCategoriesAsync(pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }


        [HttpGet(nameof(GetAnalysisCategoryById))]
        public async Task<IActionResult> GetAnalysisCategoryById(int categoryid)
        {
            try
            {
                var result = await _analysisCategoryService.GetAnalysisCategoryByIdAsync(categoryid);

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
