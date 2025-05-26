using MetricService.BLL.Dto;

namespace MetricService.BLL.Interfaces
{
    public  interface IWorkoutService
    {
        /// <summary>
        /// Обновление или создание записи о тренировке пользователя
        /// </summary>
        /// <param name="workoutDTO">параметры сна</param>
        /// <returns></returns>
        public Task<bool> UpdateWorkoutAsync(WorkoutDTO workoutDTO);

        /// <summary>
        /// удаление записи о тренировке пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns></returns>
        public Task<bool> DeleteWorkoutAsync(int userId);

        /// <summary>
        /// получить запись о тренировке пользователя
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        public Task<WorkoutDTO?> GetWorkoutByUserIdAsync(int userId, int workoutId);

        /// <summary>
        /// Получение записей о тренировках пользователя за период
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">Начало периода выборки данных</param>
        /// <param name="endDate">Конец периода выборки данных</param>
        /// <param name="pageNum">Номер страницы для пагинации</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        public Task<IEnumerable<SleepDTO>> GetAllWorkoutsByUserIdAsync(int userId, DateTime begDate, DateTime endDate, int pageNum, int pageSize);
    }
}
