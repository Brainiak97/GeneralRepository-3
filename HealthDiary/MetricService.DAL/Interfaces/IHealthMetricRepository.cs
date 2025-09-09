using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о медицинских показателях пользователя
    /// </summary>
    /// <seealso cref="HealthMetric" />
    public interface IHealthMetricRepository : IRepository<HealthMetric>
    {
    }
}
