using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о тренировках пользователя
    /// </summary>
    public interface IWorkoutService
    {
        /// <summary>
        /// Создать запись о тренировке пользователя
        /// </summary>
        /// <param name="workoutDTO">Данные для создания записи о тренировке</param>       
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>      
        public Task CreateWorkoutAsync(WorkoutCreateDTO workoutDTO);

        /// <summary>
        /// Обновить данные о тренировке пользователя
        /// </summary>
        /// <param name="workoutDTO">Данные для изменения записи о тренировке пользователя</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="ValidateModelException">Тренировка не зарегистрирована</exception>
        public Task UpdateWorkoutAsync(WorkoutUpdateDTO workoutDTO);

        /// <summary>
        /// Удалить данные о тренировке пользователя
        /// </summary>
        /// <param name="workoutId">Идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная тренировка не существует</exception>
        public Task DeleteWorkoutAsync(int workoutId);

        /// <summary>
        /// Получить запись о тренировке пользователя
        /// </summary>
        /// <param name="workoutId">Идентификатор записи</param>
        /// <returns>Запись о тренировке пользователя</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная тренировка не существует</exception>
        public Task<WorkoutDTO> GetWorkoutByIdAsync(int workoutId);

        /// <summary>
        /// Получить список тренировок пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>        
        /// <returns>Список тренировок пользователя за период</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<WorkoutDTO>> GetAllWorkoutsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);       
    }
}
