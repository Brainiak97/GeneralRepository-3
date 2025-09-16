using MetricService.BLL.Exceptions;
using MetricService.Domain.Models;

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
        /// <param name="healthMetricValue">Данные для создания записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>  
        public Task CreateHealthMetricValueAsync(HealthMetricValue healthMetricValue);

        /// <summary>
        /// Изменить запись о значении показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValue">Данные для изменения записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Запись о значении показателя здоровья пользователя не зарегистрирована</exception>
        public Task UpdateHealthMetricValueAsync(HealthMetricValue healthMetricValue);

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
        public Task<HealthMetricValue> GetHealthMetricValueByIdAsync(int healthMetricId);

        /// <summary>
        /// Получить список записей значений показателей здоровья пользователя за период
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">дата начала периода</param>
        /// <param name="endDate">дата окончания периода</param>
        /// <returns>Список записей записей значений показателей здоровья пользователя за период</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<HealthMetricValue>> GetAllHealthMetricsValueByUserIdAsync(int userId, DateTime begDate, DateTime endDate);
    }
}
