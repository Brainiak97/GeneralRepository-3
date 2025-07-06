using MetricService.Domain.Models;


namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о напоминании приема медикаментов пользователем
    /// </summary>
    /// <seealso cref="Reminder" />
    public interface IReminderRepository: IRepository<Reminder>
    {
    }
}
