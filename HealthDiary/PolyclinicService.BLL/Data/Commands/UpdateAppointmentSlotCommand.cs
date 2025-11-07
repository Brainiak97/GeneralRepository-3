namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на редактирование данных слота приёма к врачу.
/// </summary>
public record UpdateAppointmentSlotCommand
{
    /// <summary>
    /// Идентификатор приёма в графике.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int DoctorId { get; init; }
    
    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public int PolyclinicId { get; init; }

    /// <summary>
    /// Идентификатор пользователя (пациента).
    /// </summary>
    public int? UserId { get; init; }

    /// <summary>
    /// Дата приёма.
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Продолжительность приёма.
    /// </summary>
    public TimeSpan Duration { get; init; }
}