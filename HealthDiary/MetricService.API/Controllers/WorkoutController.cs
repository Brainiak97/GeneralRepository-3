using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.API.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данными о тренировках пользователя
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkoutController(IWorkoutService workoutService) : Controller
    {
        private readonly IWorkoutService _workoutService = workoutService;

        /// <summary>
        /// Зарегистрировать тренировку
        /// </summary>
        /// <param name="workoutDTO">Дянные для регистрации о тренировке</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateWorkout))]
        public async Task<IActionResult> CreateWorkout([FromBody] WorkoutCreateDTO workoutDTO)
        {
            await _workoutService.CreateWorkoutAsync(workoutDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о тренировке
        /// </summary>
        /// <param name="workoutDTO">Измененные данные о тренировке</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateWorkout))]
        public async Task<IActionResult> UpdateWorkout([FromBody] WorkoutUpdateDTO workoutDTO)
        {
            await _workoutService.UpdateWorkoutAsync(workoutDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить трениовку
        /// </summary>
        /// <param name="workoutId">Идентификатор тренировки</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteWorkoutAsync))]
        public async Task<IActionResult> DeleteWorkoutAsync(int workoutId)
        {
            await _workoutService.DeleteWorkoutAsync(workoutId);
            return Ok();
        }

        /// <summary>
        /// Получить список тренировок по пользователю за период
        /// </summary>
        /// <param name="request">Данные пользователя и период</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllWorkouts))]
        public async Task<IActionResult> GetAllWorkouts([FromQuery] RequestListWithPeriodByIdDTO request)
        {
            var result = await _workoutService.GetAllWorkoutsByUserIdAsync(request);
            
            return Ok(result);
        }

        /// <summary>
        /// Получить данные о тренировке
        /// </summary>
        /// <param name="workoutId">Идентификатор тренировки</param>
        /// <returns></returns>
        [HttpGet(nameof(GetWorkoutById))]
        public async Task<IActionResult> GetWorkoutById(int workoutId)
        {
            return Ok(await _workoutService.GetWorkoutByIdAsync(workoutId));
        }
    }
}
