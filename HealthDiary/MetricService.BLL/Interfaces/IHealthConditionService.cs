using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthCondition;
using MetricService.BLL.Exceptions;
using MetricService.Domain.Models;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о самочувствии(состоянии здоровья) пользователя
    /// </summary>
    public interface IHealthConditionService
    {
        /// <summary>
        /// Создать запись о самочувствии(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="healthCondition">Данные для создания записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>                 
        public Task CreateHealthConditionAsync(HealthCondition healthCondition);

        /// <summary>
        /// Обновить запись о самочувствии(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="healthCondition">Данные для изменения записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>        
        /// <exception cref="IncorrectOrEmptyResultException">Данные о самочувствии не зарегистрированы</exception>
        public Task UpdateHealthConditionAsync(HealthCondition healthCondition);

        /// <summary>
        /// Удалить запись о самочувствии(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="healthConditionId">Идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Данные о самочувствии не зарегистрированы</exception>
        public Task DeleteHealthConditionAsync(int healthConditionId);

        /// <summary>
        /// Получить запись о самочувствии(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="healthConditionId">Идентификатор записи</param>
        /// <returns>Запись о сне пользователя</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Данные о самочувствии не зарегистрированы</exception>
        public Task<HealthCondition> GetHealthConditionByIdAsync(int healthConditionId);

        /// <summary>
        /// Получить все записи о снах пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>                
        /// <returns>Список записей о самочувствии пользователя за период</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<HealthCondition>> GetAllHealthConditionsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
