using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о доступе к самочувствию(состоянию здоровья) пользователя
    /// </summary>
    /// <seealso cref="HealthCondition" />
    public interface IHealthConditionRepository : IRepository<HealthCondition>
    {
    }
}
