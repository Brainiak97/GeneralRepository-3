using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AccessToMetrics;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными доступа к личным метрикам пользователя
    /// </summary>
    public interface IAccessToMetricsService
    {
        /// <summary>
        /// Создать запись о доступе к личным метрикам пользователя
        /// </summary>
        /// <param name="accessToMetricsCreateDTO">Данные для создания записи</param>        
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>        
        public Task CreateAccessToMetricsAsync(AccessToMetricsCreateDTO accessToMetricsCreateDTO);

        /// <summary>
        /// Обновить запись о доступе к личным метрикам пользователя 
        /// </summary>
        /// <param name="accessToMetricsUpdateDTO">Данные для измения записи</param>        
        /// <exception cref="IncorrectOrEmptyResultException">Запись доступа к метрикам не зарегистрирована</exception>       
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>        
        public Task UpdateAccessToMetricsAsync(AccessToMetricsUpdateDTO accessToMetricsUpdateDTO);

        /// <summary>
        /// Удалить запись о доступе к личным метрикам пользователя
        /// </summary>
        /// <param name="accessToMetricsId">Идентификатор записи</param>        
        /// <exception cref="IncorrectOrEmptyResultException">Запись доступа к метрикам не зарегистрирована</exception>       
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные</exception>
        public Task DeleteAccessToMetricsAsync(int accessToMetricsId);

        /// <summary>
        /// Получить запись о доступе к личным метрикам пользователя
        /// </summary>
        /// <param name="accessToMetricsId">Идентификатор записи</param>

        /// <returns>Запись о доступе к личным метрикам пользователя</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Запись доступа к метрикам не зарегистрирована</exception>        
        public Task<AccessToMetricsDTO?> GetAccessToMetricsByIdAsync(int accessToMetricsId);

        /// <summary>
        /// Получить список доступа к личным метрикам для пользователя, предоставившего доступ
        /// </summary>
        /// <param name="requestAccessListWithPeriodByIdDTO">Данные пользователя, период и типы записей</param>        
        /// <returns>Список доступа к личным метрикам для пользователя, предоставившего доступ</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<AccessToMetricsDTO>> GetAllAccessToMetricsByProviderUserIdAsync(RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO);

        /// <summary>
        /// Получить список доступа к личным метрикам для пользователя, получившего доступ
        /// </summary>
        /// <param name="requestAccessListWithPeriodByIdDTO">Данные пользователя, период и типы записей</param>        
        /// <returns>Список доступа к личным метрикам для пользователя, получившего доступ</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<AccessToMetricsDTO>> GetAllAccessToMetricsByGrantedUserIdAsync(RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO);

        /// <summary>
        /// Проверка наличия доступа к личным метрикам
        /// </summary>
        /// <param name="providerUserId">Идентификатор пользователя предоставившый доступ</param>
        /// <param name="grantedUserId">Идентификатор пользователя получившый доступ</param>        
        /// <returns></returns>
        public Task<bool> CheckAccessToMetricsAsync(int providerUserId, int grantedUserId);
    }
}
