using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetricsBase;

namespace MetricService.BLL.Interfaces
{
    public interface IHealthMetricsBaseService 
    {
        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="healthMetricsBaseDTO">Информация о записи</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>        
        public Task CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseCreateDTO healthMetricsBaseDTO);

        /// <summary>
        /// обновить запись
        /// </summary>
        /// <param name="healthMetricsBaseDTO">информвция о записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Запись о базовых мед. показателях не зарегистрирована</exception>
        public Task UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseUpdateDTO healthMetricsBaseDTO);

        /// <summary>
        /// Удалить запись о базовых медицинских показателей
        /// </summary>
        /// <param name="healthMetricsBaseId">идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о базовых мед. показателях не существует</exception>
        public Task DeleteRecordOfHealthMetricsBaseAsync(int uhealthMetricsBaseId);

        /// <summary>
        /// получить запись о базовых показателях пользователя
        /// </summary>
        /// <param name="healthMetricsBaseId">ИД записи</param>
        /// <returns>модель DTO</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о базовых мед. показателях не существует</exception>
        public Task<HealthMetricsBaseDTO> GetRecordOfHealthMetricsBaseByIdAsync(int healthMetricsBaseId);

        /// <summary>
        /// Получить все записи для пользователя
        /// </summary>
        /// <param cref="RequestListWithPeriodByIdDTO">Запрос</param>        
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<HealthMetricsBaseDTO>> GetAllRecordsOfHealthMetricsBaseByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
