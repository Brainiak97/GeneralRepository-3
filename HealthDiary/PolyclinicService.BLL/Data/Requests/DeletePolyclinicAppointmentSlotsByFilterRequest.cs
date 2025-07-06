namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на удаление слотов на приёмы к врачу поликлиники по фильтру.
/// </summary>
public class DeletePolyclinicAppointmentSlotsByFilterRequest
{
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public int? PolyclinicId { get; set; }
    
    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int? DoctorId { get; set; }
    
    /// <summary>
    /// Дата начала временного интервала для удаления.
    /// </summary>
    public DateOnly? PeriodStartDate { get; set; }
    
    /// <summary>
    /// Дата окончания временного интервала для удаления.
    /// </summary>
    public DateOnly? PeriodEndDate { get; set; }
}
