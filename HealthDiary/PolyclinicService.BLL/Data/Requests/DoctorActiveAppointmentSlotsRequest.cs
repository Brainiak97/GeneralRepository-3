namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на получение активных слотов приёмов к врачу.
/// </summary>
public class DoctorActiveAppointmentSlotsRequest
{
    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int DoctorId { get; set; }
    
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public int? PolyclinicId { get; set; }
    
    /// <summary>
    /// Дата, на которую необходимо получить слоты приёма врача.
    /// </summary>
    public DateTime? Date { get; set; }
}