using MetricService.BLL.Dto;

namespace MetricService.BLL.Interfaces
{
    public interface IHealthMetricsBase 
    {
        /// <summary>
        /// Обновление или создание записи о базовых медицинских показателей пользователя
        /// </summary>
        /// <param name="HealthMetricsBaseDTO">параметры сна</param>
        /// <returns></returns>
        public Task<bool> UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO healthMetricsBaseDTO);

        /// <summary>
        /// удаление записи о базовых медицинских показателях пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns></returns>
        public Task<bool> DeleteRecordOfHealthMetricsBaseAsync(int userId);

        /// <summary>
        /// получить запись о базовых медицинских показателях пользователя
        /// </summary>
        /// <param name="healthMetricsBaseId"></param>
        /// <returns></returns>
        public Task<HealthMetricsBaseDTO?> GetRecordOfHealthMetricsBaseByIdAsync(int userId, int healthMetricsBaseId);

        /// <summary>
        /// Получение записей о базовых медицинских показателях пользователя за период
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">Начало периода выборки данных</param>
        /// <param name="endDate">Конец периода выборки данных</param>
        /// <param name="pageNum">Номер страницы для пагинации</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        public Task<IEnumerable<HealthMetricsBaseDTO>> GetAllRecordsOfHealthMetricsBaseByUserIdAsync(int userId, DateTime begDate, DateTime endDate, int pageNum, int pageSize);
    }
}
