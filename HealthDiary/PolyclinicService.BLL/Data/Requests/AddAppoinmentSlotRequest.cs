namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на добавление слота приёма к врачу в график поликлиники.
/// </summary>
public class AddAppoinmentSlotRequest
{
    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public required int DoctorId { get; set; }
    
    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public required int PolyclinicId { get; set; }

    /// <summary>
    /// Идентификатор пользователя (пациента).
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Дата приёма.
    /// </summary>
    public required DateOnly Date { get; set; }

    /// <summary>
    /// Время начала приёма.
    /// </summary>
    public required TimeSpan StartTime { get; set; }

    /// <summary>
    /// Время окончания приёма.
    /// </summary>
    public required TimeSpan EndTime { get; set; }
}