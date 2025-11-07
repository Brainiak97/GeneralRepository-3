namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на получение активных слотов приёмов к врачу.
/// </summary>
public record DoctorActiveAppointmentSlotsCommand
{
    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int DoctorId { get; init; }

    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public int? PolyclinicId { get; init; }

    /// <summary>
    /// Дата, на которую необходимо получить слоты приёма врача.
    /// </summary>
    public DateTime? Date { get; init; }
}