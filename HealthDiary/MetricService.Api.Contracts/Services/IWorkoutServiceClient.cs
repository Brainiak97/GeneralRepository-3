using MetricService.Api.Contracts.Dtos.Common;
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
        /// <param name="createDTO">Дянные для регистрации о тренировке</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateWorkout)}")]
        Task CreateWorkout(WorkoutCreateDTO createDTO);

        /// <summary>
        /// Изменить данные о тренировке
        /// </summary>
        /// <param name="updateDTO">Измененные данные о тренировке</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateWorkout)}")]
        Task UpdateWorkout(WorkoutUpdateDTO updateDTO);

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
        /// <param name="requestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllWorkouts)}")]
        Task<IEnumerable<WorkoutDTO>> GetAllWorkouts(RequestListWithPeriodByIdDTO requestDTO);

        /// <summary>
        /// Получить данные о тренировке
        /// </summary>
        /// <param name="workoutId">Идентификатор тренировки</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetWorkoutById)}")]
        Task<WorkoutDTO> GetWorkoutById(int workoutId);
    }
}
