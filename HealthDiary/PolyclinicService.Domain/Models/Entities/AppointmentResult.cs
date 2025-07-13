using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Результат приёма у врача.
/// </summary>
public class AppointmentResult : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор результата приёма.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Содержание отчёта приёма пациента.
    /// </summary>
    public required string ReportContent { get; set; }
    
    /// <summary>
    /// Идентификатор слота на приём к врачу.
    /// </summary>
    public int AppointmentSlotId { get; set; }
    
    /// <summary>
    /// Идентификатор шаблона отчёта.
    /// </summary>
    public int ReportTemplateId { get; set; }

    /// <summary>
    /// Данные о приёме к врачу по графику поликлиники.
    /// </summary>
    public AppointmentSlot AppointmentSlot { get; set; } = null!;
}