using MetricService.Domain.Models;


namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о схеме приема медикаментов пользователем
    /// </summary>
    /// <seealso cref="Regimen" />
    public interface IRegimenRepository : IRepository<Regimen>
    {
    }
}
