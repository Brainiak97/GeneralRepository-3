using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkoutController(IWorkoutService workoutService) : Controller
    {
        private readonly IWorkoutService _workoutService = workoutService;

        [HttpPost(nameof(CreateWorkout))]
        public async Task<IActionResult> CreateWorkout([FromBody] WorkoutCreateDTO workoutDTO)
        {
            await _workoutService.CreateWorkoutAsync(workoutDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateWorkout))]
        public async Task<IActionResult> UpdateWorkout([FromBody] WorkoutUpdateDTO workoutDTO)
        {
            await _workoutService.UpdateWorkoutAsync(workoutDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteWorkoutAsync))]
        public async Task<IActionResult> DeleteWorkoutAsync(int id)
        {
            await _workoutService.DeleteWorkoutAsync(id);
            return Ok();
        }

        [HttpGet(nameof(GetAllWorkouts))]
        public async Task<IActionResult> GetAllWorkouts([FromQuery] RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _workoutService.GetAllWorkoutsByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        [HttpGet(nameof(GetWorkoutById))]
        public async Task<IActionResult> GetWorkoutById(int workoutid)
        {
            return Ok(await _workoutService.GetWorkoutByIdAsync(workoutid));
        }
    }
}
