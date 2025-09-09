using MetricService.BLL.DTO.HealthMetric;
using MetricService.BLL.Exceptions;

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
        /// <param name="healthMetricCreateDTO">Данные для создания записи</param>
        public Task CreateHealthMetricAsync(HealthMetricCreateDTO healthMetricCreateDTO);

        /// <summary>
        /// Изменить запись о показателях здоровья пользователя
        /// </summary>
        /// <param name="healthMetricUpdateDTO">Данные для изменения записи</param>              
        /// <exception cref="IncorrectOrEmptyResultException">Запись о показателях здоровья пользователя не зарегистрирована</exception>
        /// <exception cref="ReferenceToEntryException">На сущность имеются ссылки</exception>
        public Task UpdateHealthMetricAsync(HealthMetricUpdateDTO healthMetricUpdateDTO);

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
        public Task<HealthMetricDTO> GetHealthMetricByIdAsync(int healthMetricId);

        /// <summary>
        /// Получить список показателей здоровья пользователя
        /// </summary>        
        /// <returns>Список показателей здоровья пользователя</returns>
        public Task<IEnumerable<HealthMetricDTO>> GetAllHealthMetricsAsync();
    }
}
