namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на получение всех слотов приёмов к врачам поликлиники на дату.
/// </summary>
public record PolyclinicAppointmentSlotsByDateCommand
{
    /// <summary>
    /// Дата, на которую необходимо получить слоты приёма.
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public required int PolyclinicId { get; init; }
}