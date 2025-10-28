using MetricService.Api.Contracts.Dtos;
using MetricService.Api.Contracts.Dtos.Workout;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данными о тренировках пользователя
    /// </summary>     
    public interface IWorkoutServiceClient
    {       
        const string Controller = "/Workout";
        /// <summary>
        /// Зарегистрировать тренировку
        /// </summary>
        /// <param name="workoutDTO">Дянные для регистрации о тренировке</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateWorkout)}")]
        Task CreateWorkout(WorkoutCreateDTO workoutDTO);

        /// <summary>
        /// Изменить данные о тренировке
        /// </summary>
        /// <param name="workoutDTO">Измененные данные о тренировке</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateWorkout)}")]
        Task UpdateWorkout(WorkoutUpdateDTO workoutDTO);

        /// <summary>
        /// Удалить трениовку
        /// </summary>
        /// <param name="workoutId">Идентификатор тренировки</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteWorkoutAsync)}")]
        Task DeleteWorkoutAsync(int workoutId);

        /// <summary>
        /// Получить список тренировок по пользователю за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllWorkouts)}")]
        Task<IEnumerable<WorkoutDTO>> GetAllWorkouts(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);

        /// <summary>
        /// Получить данные о тренировке
        /// </summary>
        /// <param name="workoutId">Идентификатор тренировки</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetWorkoutById)}")]
        Task<WorkoutDTO> GetWorkoutById(int workoutId);
    }
}
