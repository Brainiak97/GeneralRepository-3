using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Слот приёма к врачу в графике поликлиники.
/// </summary>
[Comment("Слоты приёма к врачу в графике поликлиники")]
public class AppointmentSlot : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор слота приёма в графике.
    /// </summary>
    [Comment("Идентификатор слота приёма в графике")]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    [Comment("Идентификатор врача")]
    public int DoctorId { get; set; }

    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    [Comment("Идентификатор поликлинники")]
    public int PolyclinicId { get; set; }

    /// <summary>
    /// Идентификатор пользователя (пациента).
    /// </summary>
    [Comment("Идентификатор пользователя (пациента)")]
    public int? UserId { get; set; }

    /// <summary>
    /// Дата приёма.
    /// </summary>
    [Comment("Дата приёма")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Продолжительность приёма.
    /// </summary>
    [Comment("Продолжительность приёма")]
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Статус приёма в графике.
    /// </summary>
    [Comment("Статус приёма в графике")]
    public AppointmentSlotStatus Status { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с врачом.
    /// </summary>
    public Doctor Doctor { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство для связи с поликлиникой.
    /// </summary>
    public Polyclinic Polyclinic { get; set; } = null!;
}