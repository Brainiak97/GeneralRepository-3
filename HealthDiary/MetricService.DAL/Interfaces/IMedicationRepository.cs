using MetricService.Domain.Models;


namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о медикаменте
    /// </summary>
    /// <seealso cref="Medication" />
    public interface IMedicationRepository : IRepository<Medication>
    {
    }
}
