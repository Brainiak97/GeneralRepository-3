namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на получение всех слотов приёмов к врачам поликлиники на дату.
/// </summary>
public class PolyclinicAppointmentSlotsByDateRequest
{
    /// <summary>
    /// Дата, на которую необходимо получить слоты приёма.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public required int PolyclinicId { get; set; }    
}