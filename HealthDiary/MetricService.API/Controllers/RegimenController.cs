using MetricService.BLL.DTO.Workout;
using MetricService.BLL.DTO;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetricService.BLL.DTO.Regimen;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegimenController(IRegimenService regimenService) : Controller
    {
        private readonly IRegimenService _regimenService = regimenService;

        [HttpPost(nameof(CreateRegimen))]
        public async Task<IActionResult> CreateRegimen([FromBody] RegimenCreateDTO regimenCreateDTO)
        {
            await _regimenService.CreateRegimenAsync(regimenCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateRegimen))]
        public async Task<IActionResult> UpdateRegimen([FromBody] RegimenUpdateDTO regimenUpdateDTO)
        {
            await _regimenService.UpdateRegimenAsync(regimenUpdateDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteRegimenAsync))]
        public async Task<IActionResult> DeleteRegimenAsync(int id)
        {
            await _regimenService.DeleteRegimenAsync(id);
            return Ok();
        }

        [HttpGet(nameof(GetAllRegimens))]
        public async Task<IActionResult> GetAllRegimens([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _regimenService.GetAllRegimenByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        [HttpGet(nameof(GetRegimenById))]
        public async Task<IActionResult> GetRegimenById(int regimenid)
        {
            return Ok(await _regimenService.GetRegimenByIdAsync(regimenid));
        }
    }
}
