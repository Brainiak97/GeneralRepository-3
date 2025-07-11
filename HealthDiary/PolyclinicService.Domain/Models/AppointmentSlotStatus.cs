namespace PolyclinicService.Domain.Models;

/// <summary>
/// Статусы приемов к врачу.
/// </summary>
public enum AppointmentSlotStatus : short
{
    /// <summary>
    /// Создан.
    /// </summary>
    Created = 1,

    /// <summary>
    /// Зарезервирован.
    /// </summary>
    Booked = 2,

    /// <summary>
    /// Закрыт. (после посещения пациентом).
    /// </summary>
    Closed = 3,
}