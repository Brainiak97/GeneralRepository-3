using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DosageFormController(IDosageFormService dosageFormService) : Controller
    {
        readonly IDosageFormService _dosageFormService = dosageFormService;

        [HttpPost(nameof(CreateDosageForm))]
        [Authorize]
        public async Task<IActionResult> CreateDosageForm([FromBody] DosageFormCreateDTO dosageFormCreateDTO)
        {
            await _dosageFormService.CreateDosageFormAsync(dosageFormCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateDosageForm))]
        [Authorize]
        public async Task<IActionResult> UpdateDosageForm([FromBody] DosageFormUpdateDTO dosageFormUpdateDTO)
        {
            await _dosageFormService.UpdateDosageFormAsync(dosageFormUpdateDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteDosageForm))]
        [Authorize]
        public async Task<IActionResult> DeleteDosageForm(int id)
        {
            await _dosageFormService.DeleteDosageFormAsync(id);
            return Ok();
        }


        [HttpGet(nameof(GetAllDosageForms))]
        public async Task<IActionResult> GetAllDosageForms(int pagenum, int pagesize)
        {
            var result = await _dosageFormService.GetAllDosageFormsAsync(pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }


        [HttpGet(nameof(GetDosageFormById))]
        public async Task<IActionResult> GetDosageFormById(int id)
        {
            var result = await _dosageFormService.GetDosageFormByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
