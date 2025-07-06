using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Данные о приёме к врачу в графике поликлиники.
/// </summary>
public class AppointmentSlot : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор приёма в графике.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int DoctorId { get; set; }
    
    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public int PolyclinicId { get; set; }

    /// <summary>
    /// Идентификатор пользователя (пациента).
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Дата приёма.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Время начала приёма.
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Время окончания приёма.
    /// </summary>
    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// Статус приёма в графике.
    /// </summary>
    public AppointmentSlotStatus Status { get; set; }
}