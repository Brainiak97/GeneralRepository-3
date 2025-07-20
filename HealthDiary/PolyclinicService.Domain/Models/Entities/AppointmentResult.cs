using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Результат приёма у врача.
/// </summary>
[Comment("Результаты приёмов пациентов")]
public class AppointmentResult : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор результата приёма.
    /// </summary>
    [Comment("Идентификатор результата приёма")]
    public int Id { get; set; }

    /// <summary>
    /// Содержание отчёта по приёму пациента.
    /// </summary>
    [Comment("Содержание отчёта по приёму пациента")]
    public required string ReportContent { get; set; }
    
    /// <summary>
    /// Идентификатор слота на приём к врачу из графика.
    /// </summary>
    [Comment("Идентификатор слота на приём к врачу из графика")]
    public int AppointmentSlotId { get; set; }
    
    /// <summary>
    /// Идентификатор шаблона отчёта.
    /// </summary>
    [Comment("Идентификатор шаблона отчёта")]
    public int ReportTemplateId { get; set; }

    /// <summary>
    /// Данные о приёме к врачу по графику поликлиники.
    /// </summary>
    public AppointmentSlot AppointmentSlot { get; set; } = null!;
}