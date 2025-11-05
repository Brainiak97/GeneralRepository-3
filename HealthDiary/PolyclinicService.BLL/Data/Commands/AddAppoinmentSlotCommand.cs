namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на добавление слота приёма к врачу в график поликлиники.
/// </summary>
public record AddAppoinmentSlotCommand
{
    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public required int DoctorId { get; init; }

    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public required int PolyclinicId { get; init; }

    /// <summary>
    /// Идентификатор пользователя (пациента).
    /// </summary>
    public int? UserId { get; init; }

    /// <summary>
    /// Дата приёма.
    /// </summary>
    public required DateTime Date { get; init; }

    /// <summary>
    /// Продолжительность приёма.
    /// </summary>
    public TimeSpan Duration { get; init; }
}