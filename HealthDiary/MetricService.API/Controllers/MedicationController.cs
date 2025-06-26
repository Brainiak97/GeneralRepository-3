using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.MedicationDTO;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController(IMedicationService medicationService) : Controller
    {

        readonly IMedicationService _medicationService = medicationService;

        [HttpPost(nameof(CreateMedication))]
        [Authorize]
        public async Task<IActionResult> CreateMedication([FromBody] MedicationCreateDTO medicationCreateDTO)
        {
            await _medicationService.CreateMedicationAsync(medicationCreateDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateMedication))]
        [Authorize]
        public async Task<IActionResult> UpdateMedication([FromBody] MedicationUpdateDTO medicationUpdateDTO)
        {
            await _medicationService.UpdateMedicationAsync(medicationUpdateDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteMedication))]
        [Authorize]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            await _medicationService.DeleteMedicationAsync(id);
            return Ok();
        }


        [HttpGet(nameof(GetAllMedications))]
        public async Task<IActionResult> GetAllMedications(int pagenum, int pagesize)
        {
            var result = await _medicationService.GetAllMedicationAsync(pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }


        [HttpGet(nameof(GetMedicationById))]
        public async Task<IActionResult> GetMedicationById(int medicationid)
        {
            var result = await _medicationService.GetMedicationByIdAsync(medicationid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
