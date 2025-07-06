using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о базовых медицинских показателях пользователя
    /// </summary>
    /// <seealso cref="HealthMetricsBase" />
    public interface IHealthMetricsBaseRepository : IRepository<HealthMetricsBase> { }
}
