namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на редактирование данных слота приёма к врачу.
/// </summary>
public class UpdateAppointmentSlotRequest
{
    /// <summary>
    /// Идентификатор приёма в графике.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int? DoctorId { get; set; }
    
    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public int? PolyclinicId { get; set; }

    /// <summary>
    /// Идентификатор пользователя (пациента).
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Дата приёма.
    /// </summary>
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Время начала приёма.
    /// </summary>
    public TimeSpan? StartTime { get; set; }

    /// <summary>
    /// Время окончания приёма.
    /// </summary>
    public TimeSpan? EndTime { get; set; }
}