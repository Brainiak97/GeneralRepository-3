namespace PolyclinicService.BLL.Data.Dtos;

/// <summary>
/// Результат приёма.
/// </summary>
public class AppointmentResultDto
{
    /// <summary>
    /// Идентификатор результата приёма.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Содержание отчёта по приёму пациента.
    /// </summary>
    public required string ReportContent { get; set; }

    /// <summary>
    /// Идентификатор слота на приём к врачу из графика.
    /// </summary>
    public required int AppointmentSlotId { get; set; }

    /// <summary>
    /// Идентификатор шаблона отчёта.
    /// </summary>
    public required int ReportTemplateId { get; set; }
}