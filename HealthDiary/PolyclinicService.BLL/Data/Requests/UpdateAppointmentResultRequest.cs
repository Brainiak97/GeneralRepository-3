namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на редактирование результата приёма.
/// </summary>
public class UpdateAppointmentResultRequest
{
    /// <summary>
    /// Идентификатор результата приёма.
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Содержание отчёта по приёму пациента.
    /// </summary>
    public string? ReportContent { get; set; }
    
    /// <summary>
    /// Идентификатор слота на приём к врачу из графика.
    /// </summary>
    public int? AppointmentSlotId { get; set; }
    
    /// <summary>
    /// Идентификатор шаблона отчёта.
    /// </summary>
    public int? ReportTemplateId { get; set; }
}