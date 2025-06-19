using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Intake;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntakeController(IIntakeService intakeService) : Controller
    {
        readonly IIntakeService _intakeService = intakeService;

        [HttpPost(nameof(CreateIntake))]
        [Authorize]
        public async Task<IActionResult> CreateIntake([FromBody] IntakeCreateDTO intakeDTO)
        {
            await _intakeService.CreateIntakeAsync(intakeDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateIntake))]
        [Authorize]
        public async Task<IActionResult> UpdateIntake([FromBody] IntakeUpdateDTO intakeUpdateDTO)
        {
            await _intakeService.UpdateIntakeAsync(intakeUpdateDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteIntake))]
        [Authorize]
        public async Task<IActionResult> DeleteIntake(int id)
        {
            await _intakeService.DeleteIntakeAsync(id);
            return Ok();
        }


        [HttpGet(nameof(GetAllIntakes))]
        public async Task<IActionResult> GetAllIntakes([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _intakeService.GetAllIntakeByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }


        [HttpGet(nameof(GetIntakeById))]
        public async Task<IActionResult> GetIntakeById(int id)
        {
            var result = await _intakeService.GetIntakeByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
