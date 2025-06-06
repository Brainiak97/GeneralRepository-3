using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Exceptions;
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

        [HttpPost("CreateWorkout")]
        public async Task<IActionResult> CreateWorkout([FromBody] WorkoutCreateDTO workoutDTO)
        {
            try
            {
                await _workoutService.CreateWorkoutAsync(workoutDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpPost("UpdateWorkout")]
        public async Task<IActionResult> UpdateWorkout([FromBody] WorkoutUpdateDTO workoutDTO)
        {
            try
            {
                await _workoutService.UpdateWorkoutAsync(workoutDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpDelete("DeleteWorkout")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            try
            {
                await _workoutService.DeleteWorkoutAsync(id);
                return Ok();
            }catch(BaseException ex)
            {
                return BadRequest(ex.GetError());
            }            
        }

        [HttpGet("GetAllWorkouts")]
        public async Task<IActionResult> GetAllWorkouts([FromQuery]RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            var result = await _workoutService.GetAllWorkoutsByUserIdAsync(requestListWithPeriodByIdDTO);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }

        [HttpGet("GetWorkoutById")]
        public async Task<IActionResult> GetWorkoutByIds(int workoutid)
        {
            try
            {
                return Ok(await _workoutService.GetWorkoutByIdAsync(workoutid));
            }catch(BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }
    }
}
