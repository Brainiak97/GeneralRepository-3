using MetricService.BLL.Exceptions;
using MetricService.Domain.Models;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о показателях здоровья пользователя
    /// </summary>
    public interface IHealthMetricService
    {
        /// <summary>
        /// Создать запись о показателях здоровья пользователя
        /// </summary>
        /// <param name="healthMetric">Данные для создания записи</param>
        public Task CreateHealthMetricAsync(HealthMetric healthMetric);

        /// <summary>
        /// Изменить запись о показателях здоровья пользователя
        /// </summary>
        /// <param name="healthMetric">Данные для изменения записи</param>              
        /// <exception cref="IncorrectOrEmptyResultException">Запись о показателях здоровья пользователя не зарегистрирована</exception>        
        public Task UpdateHealthMetricAsync(HealthMetric healthMetric);

        /// <summary>
        /// Удалить запись о показателях здоровья пользователя
        /// </summary>
        /// <param name="healthMetricId">Идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о показателях здоровья пользователя не существует</exception>
        public Task DeleteHealthMetricAsync(int healthMetricId);

        /// <summary>
        /// Получить запись о показателях здоровья пользователя
        /// </summary>
        /// <param name="healthMetricId">Идентификатор записи</param>
        /// <returns>Данные записи о показателях здоровья пользователя</returns>       
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о показателях здоровья пользователя не существует</exception>
        public Task<HealthMetric> GetHealthMetricByIdAsync(int healthMetricId);

        /// <summary>
        /// Получить список показателей здоровья пользователя
        /// </summary>        
        /// <returns>Список показателей здоровья пользователя</returns>
        public Task<IEnumerable<HealthMetric>> GetAllHealthMetricsAsync();
    }
}
