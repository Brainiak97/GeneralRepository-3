using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Workout;

namespace MetricService.BLL.Interfaces
{
    public  interface IWorkoutService
    {
        /// <summary>
        /// Создание тренировки
        /// </summary>
        /// <param name="workoutDTO">тренировка</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>       
        public Task CreateWorkoutAsync(WorkoutCreateDTO workoutDTO);

        /// <summary>
        /// Обновить данные о тренировке
        /// </summary>
        /// <param name="workoutDTO">тренировка</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="ValidateModelException">Тренировка не зарегистрирована</exception>
        public Task UpdateWorkoutAsync(WorkoutUpdateDTO workoutDTO);

        /// <summary>
        /// Удалить тренировку
        /// </summary>
        /// <param name="workoutId">ИД тренировки</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная тренировка не существует</exception>
        public Task DeleteWorkoutAsync(int userId);

        /// <summary>
        /// Получить тренировку по ИД
        /// </summary>
        /// <param name="workoutId">ИД тренировки</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная тренировка не существует</exception>
        public Task<WorkoutDTO> GetWorkoutByIdAsync(int workoutId);

        /// <summary>
        /// Получить все тренировки для пользователя
        /// </summary>
        /// <param cref="RequestListWithPeriodByIdDTO">запрос</param>        
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<WorkoutDTO>> GetAllWorkoutsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);       
    }
}
