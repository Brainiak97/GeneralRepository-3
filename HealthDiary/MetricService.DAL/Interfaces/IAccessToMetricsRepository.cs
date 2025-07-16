using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о доступе к метрикам пользователя для других пользователей
    /// </summary>
    /// <seealso cref="AccessToMetrics" />
    public interface IAccessToMetricsRepository: IRepository<AccessToMetrics>
    {
        /// <summary>
        /// Проверка наличия доступа к личным метрикам
        /// </summary>
        /// <param name="providerUserId">Идентификатор пользователя предоставившый доступ</param>
        /// <param name="grantedUserId">Идентификатор пользователя получившый доступ</param>        
        /// <returns></returns>
        public Task<bool> CheckAccessToMetricsAsync(int providerUserId, int grantedUserId);
    }
}
