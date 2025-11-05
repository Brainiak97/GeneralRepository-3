using System.Linq.Expressions;
using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Interfaces;

namespace PolyclinicService.DAL.Interfaces;

/// <summary>
/// Предоставляет методы для работы с приёмами к врачу в графике <see cref="AppointmentSlot"/> в базы данных.
/// </summary>
public interface IAppointmentSlotsRepository : IRepository<AppointmentSlot, int>
{
    /// <summary>
    /// Создать набор слотов на приём к врачу.
    /// </summary>
    /// <param name="appointmentSlots">Перечисление слотов приёма к врачу.</param>
    /// <returns>Признак успешного создания слотов в базе данных.</returns>
    Task<bool> AddBatchAsync(AppointmentSlot[] appointmentSlots);

    /// <summary>
    /// Вернуть слоты на приём к врачу по фильтру.
    /// </summary>
    /// <param name="filter">Выражение для фильтрации слотов в запросе.</param>
    /// <returns>Возвращает коллекцию слотов по фильтру или <see langword="null"/>.</returns>
    Task<IEnumerable<AppointmentSlot>?> GetByFilterAsync(Expression<Func<AppointmentSlot, bool>> filter);

    /// <summary>
    /// Удалить слоты на приём к врачу по фильтру.
    /// </summary>
    /// <param name="filter">Выражение для фильтрации слотов в запросе.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task DeleteByFilterAsync(Expression<Func<AppointmentSlot, bool>> filter);
}
