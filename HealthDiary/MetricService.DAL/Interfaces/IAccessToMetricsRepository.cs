using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о доступе к метрикам пользователя для других пользователей
    /// </summary>
    /// <seealso cref="AccessToMetrics" />
    public interface IAccessToMetricsRepository: IRepository<AccessToMetrics>
    {
    }
}
