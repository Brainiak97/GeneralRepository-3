namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на сохранение результата приёма.
/// </summary>
public class SaveAppointmentResultRequest
{
    /// <summary>
    /// Идентификатор слота приёма в графике.
    /// </summary>
    public required int AppointmentSlotId { get; set; }
    
    /// <summary>
    /// Содержание отчёта по приёму пациента.
    /// </summary>
    public required string ReportContent { get; set; }
    
    /// <summary>
    /// Идентификатор шаблона отчёта.
    /// </summary>
    public required int ReportTemplateId { get; set; }
    
    /// <summary>
    /// Признак необходимости отправки отчёта по почте.
    /// </summary>
    public bool NeedSendToEmail { get; set; }
}