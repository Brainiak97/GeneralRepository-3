using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о тренировке пользователя
    /// </summary>
    /// <seealso cref="Workout" />
    public interface IWorkoutRepository : IRepository<Workout> { }
}
