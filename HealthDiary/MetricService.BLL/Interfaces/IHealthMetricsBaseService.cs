using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о базовых медицинских показателях пользователя
    /// </summary>
    public interface IHealthMetricsBaseService 
    {
        /// <summary>
        /// Создать запись о базовых медицинских показателях
        /// </summary>
        /// <param name="healthMetricsBaseDTO">Данные для создания записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>  
        public Task CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseCreateDTO healthMetricsBaseDTO);

        /// <summary>
        /// Изменить запись о базовых медицинских показателях
        /// </summary>
        /// <param name="healthMetricsBaseDTO">Данные для изменения записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Запись о базовых мед. показателях не зарегистрирована</exception>
        public Task UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseUpdateDTO healthMetricsBaseDTO);

        /// <summary>
        /// Удалить запись о базовых медицинских показателей
        /// </summary>
        /// <param name="healthMetricsBaseId">Идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о базовых мед. показателях не существует</exception>
        public Task DeleteRecordOfHealthMetricsBaseAsync(int healthMetricsBaseId);

        /// <summary>
        /// Получить запись о базовых показателях пользователя
        /// </summary>
        /// <param name="healthMetricsBaseId">Идентификатор записи</param>
        /// <returns>Данные записи о базовых медицинских показателях пользователя</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о базовых мед. показателях не существует</exception>
        public Task<HealthMetricsBaseDTO> GetRecordOfHealthMetricsBaseByIdAsync(int healthMetricsBaseId);

        /// <summary>
        /// Получить список записей о базовых показателях пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>        
        /// <returns>Список записей о базовых показателях пользователя за период</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<HealthMetricsBaseDTO>> GetAllRecordsOfHealthMetricsBaseByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
