namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на удаление слотов на приёмы к врачу поликлиники по фильтру.
/// </summary>
public record DeletePolyclinicAppointmentSlotsByFilterCommand
{
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public int? PolyclinicId { get; init; }

    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int? DoctorId { get; init; }

    /// <summary>
    /// Дата начала временного интервала для удаления.
    /// </summary>
    public DateTime? PeriodStartDate { get; init; }

    /// <summary>
    /// Дата окончания временного интервала для удаления.
    /// </summary>
    public DateTime? PeriodEndDate { get; init; }
}
