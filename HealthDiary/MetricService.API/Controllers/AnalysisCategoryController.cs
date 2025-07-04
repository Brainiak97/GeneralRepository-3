using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisCategoryController(IAnalysisCategoryService analysisCategoryService) : Controller
    {

        readonly IAnalysisCategoryService _analysisCategoryService = analysisCategoryService;

        [HttpPost(nameof(CreateAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> CreateAnalysisCategory([FromBody] AnalysisCategoryCreateDTO analysisCategoryCreateDTO)
        {
            await _analysisCategoryService.CreateAnalysisCategoryAsync(analysisCategoryCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> UpdateAnalysisCategory([FromBody] AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO)
        {
            await _analysisCategoryService.UpdateAnalysisCategoryAsync(analysisCategoryUpdateDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteAnalysisCategory))]
        [Authorize]
        public async Task<IActionResult> DeleteAnalysisCategory(int id)
        {
            await _analysisCategoryService.DeleteAnalysisCategoryAsync(id);
            return Ok();
        }


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


        [HttpGet(nameof(GetAnalysisCategoryById))]
        public async Task<IActionResult> GetAnalysisCategoryById(int categoryid)
        {
            var result = await _analysisCategoryService.GetAnalysisCategoryByIdAsync(categoryid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
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
