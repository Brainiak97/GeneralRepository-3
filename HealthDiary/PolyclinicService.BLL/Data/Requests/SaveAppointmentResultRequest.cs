namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на сохранение результата приёма.
/// </summary>
public class SaveAppointmentResultRequest
{
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