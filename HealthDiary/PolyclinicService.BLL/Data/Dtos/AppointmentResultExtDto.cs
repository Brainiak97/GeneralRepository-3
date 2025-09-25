namespace PolyclinicService.BLL.Data.Dtos;

/// <summary>
/// Расширенная модель результата приёма.
/// </summary>
public class AppointmentResultExtDto : AppointmentResultDto
{
    /// <summary>
    /// Данные по слоту приёма из графика поликлиники.
    /// </summary>
    public required AppointmentSlotDto SlotInfo { get; set; }
}