using MetricService.Domain.Models;


namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о приеме медикаментов пользователем
    /// </summary>
    /// <seealso cref="Intake" />
    public interface IIntakeRepository : IRepository<Intake>
    {
    }
}
