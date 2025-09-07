using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetric;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными значений показателей здоровья пользователя
    /// </summary>
    public interface IHealthMetricValueService
    {
        /// <summary>
        /// Создать запись о значении показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueCreateDTO">Данные для создания записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>  
        public Task CreateHealthMetricValueAsync(HealthMetricValueCreateDTO healthMetricValueCreateDTO);

        /// <summary>
        /// Изменить запись о значении показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueUpdateDTO">Данные для изменения записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Запись о значении показателя здоровья пользователя не зарегистрирована</exception>
        public Task UpdateHealthMetricValueAsync(HealthMetricValueUpdateDTO healthMetricValueUpdateDTO);

        /// <summary>
        /// Удалить запись о значении показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueId">Идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о значении показателя здоровья пользователя не существует</exception>
        public Task DeleteHealthMetricValueAsync(int healthMetricValueId);

        /// <summary>
        /// Получить запись о значении показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricId">Идентификатор записи</param>
        /// <returns>Данные записи о значении показателя здоровья пользователя</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о значении показателя здоровья пользователя не существует</exception>
        public Task<HealthMetricValueDTO> GetHealthMetricValueByIdAsync(int healthMetricId);

        /// <summary>
        /// Получить список записей значений показателей здоровья пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>        
        /// <returns>Список записей записей значений показателей здоровья пользователя за период</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<HealthMetricValueDTO>> GetAllHealthMetricsValueByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
