using PolyclinicService.Domain.Models;

namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на редактирование статуса приёма к врачу в графике поликлиники.
/// </summary>
public record UpdateAppointmentSlotStatusCommand
{
    /// <summary>
    /// Идентификатор слота приёма к врачу.
    /// </summary>
    public int AppointmentSlotId { get; init; }
    
    /// <summary>
    /// Новый статус слота.
    /// </summary>
    public AppointmentSlotStatus Status { get; init; }
}