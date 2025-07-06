namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на добавление слотов приёмов к врачу по шаблону (не более месяца).
/// </summary>
public class AddAppointmentSlotsByTemplateRequest
{
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public required int PolyclinicId { get; set; }
    
    /// <summary>
    /// Идентификатор врачей, для которых добавляются слоты.
    /// </summary>
    public required int[] DoctorIds { get; set; }
    
    /// <summary>
    /// Дата начала периода для формирования слотов.
    /// </summary>
    public required DateOnly PeriodStartDate { get; set; }
    
    /// <summary>
    /// Дата окончания периода для формирования слотов.
    /// </summary>
    public required DateOnly PeriodEndDate { get; set; }
    
    /// <summary>
    /// Продолжительность приёма.
    /// </summary>
    public TimeSpan AppointmentDuration { get; set; }
    
    /// <summary>
    /// Время начала рабочего дня врача.
    /// </summary>
    public TimeSpan WorkDayStartTime { get; set; }
    
    /// <summary>
    /// Время окончания рабочего дня врача.
    /// </summary>
    public TimeSpan WorkDayEndTime { get; set; }
    
    /// <summary>
    /// Продолжительность обеда.
    /// </summary>
    public TimeSpan? LunchDuration { get; set; }

    /// <summary>
    /// Выходные дни, включённые в расчёт.
    /// </summary>
    public DayOfWeek[] IncludedWeekendDays { get; set; } = [];
}
