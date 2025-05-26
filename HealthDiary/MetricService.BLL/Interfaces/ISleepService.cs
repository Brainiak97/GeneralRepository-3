using MetricService.BLL.Dto;

namespace MetricService.BLL.Interfaces
{
    public  interface ISleepService
    {
        /// <summary>
        /// Обновление или создание записи о сне пользователя
        /// </summary>
        /// <param name="sleepDTO">параметры сна</param>
        /// <returns></returns>
        public Task<bool> UpdateRecordOfSleepAsync(SleepDTO sleepDTO);

        /// <summary>
        /// удаление записи о сне пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns></returns>
        public Task<bool> DeleteRecordOfSleepAsync(int userId);

        /// <summary>
        /// получить запись о сне пользователя
        /// </summary>
        /// <param name="sleepId"></param>
        /// <returns></returns>
        public Task<SleepDTO?> GetRecordOfSleepByIdAsync(int userId, int sleepId);

        /// <summary>
        /// Получение записей о снах пользователя за период
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">Начало периода выборки данных</param>
        /// <param name="endDate">Конец периода выборки данных</param>
        /// <param name="pageNum">Номер страницы для пагинации</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        public Task<IEnumerable<SleepDTO>> GetAllRecordsOfSleepByUserIdAsync(int userId, DateTime begDate, DateTime endDate, int pageNum, int pageSize);

    }
}
