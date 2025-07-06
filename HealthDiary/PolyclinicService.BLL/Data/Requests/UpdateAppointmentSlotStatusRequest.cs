using PolyclinicService.Domain.Models;

namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на редактирование статуса приёма к врачу в графике поликлиники.
/// </summary>
public class UpdateAppointmentSlotStatusRequest
{
    /// <summary>
    /// Идентификатор слота приёма к врачу.
    /// </summary>
    public int AppointmentSlotId { get; set; }
    
    /// <summary>
    /// Новый статус слота.
    /// </summary>
    public AppointmentSlotStatus Status { get; set; }
}