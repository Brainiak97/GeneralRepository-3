using MetricService.BLL.Dto;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WorkoutController(IWorkoutService workoutService) : Controller
    {
        private readonly IWorkoutService _workoutService = workoutService;

        [HttpPost("SaveWorkout")]
        public async Task<IActionResult> SaveWorkout([FromBody] WorkoutDTO workoutDTO)
        {
            if (workoutDTO.Id == 0)
            {
                return Ok(await _workoutService.CreateWorkoutAsync(workoutDTO));
            }
            else
            {
                return Ok(await _workoutService.UpdateWorkoutAsync(workoutDTO));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var responce = await _workoutService.DeleteWorkoutAsync(id);
            if (responce)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllWorkouts")]
        public async Task<IActionResult> GetAllWorkouts(int userid, DateTime begDate, DateTime endDate, int pagenum, int pagesize)
        {
            var result = await _workoutService.GetAllWorkoutsByUserIdAsync(userid, begDate, endDate, pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }

        [HttpGet("GetWorkoutById")]
        public async Task<IActionResult> GetWorkoutByIds(int workoutid)
        {
            var result = await _workoutService.GetWorkoutByWorkoutIdAsync(workoutid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


    }
}
