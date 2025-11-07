namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на добавление слотов приёмов к врачу по шаблону (не более месяца).
/// </summary>
public record AddAppointmentSlotsByTemplateCommand
{
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public required int PolyclinicId { get; init; }

    /// <summary>
    /// Идентификатор врачей, для которых добавляются слоты.
    /// </summary>
    public required int[] DoctorIds { get; init; }

    /// <summary>
    /// Дата начала периода для формирования слотов.
    /// </summary>
    public required DateOnly PeriodStartDate { get; init; }

    /// <summary>
    /// Дата окончания периода для формирования слотов.
    /// </summary>
    public required DateOnly PeriodEndDate { get; init; }

    /// <summary>
    /// Продолжительность приёма.
    /// </summary>
    public TimeSpan AppointmentDuration { get; init; }

    /// <summary>
    /// Время начала рабочего дня врача.
    /// </summary>
    public TimeOnly WorkDayStartTime { get; init; }

    /// <summary>
    /// Время окончания рабочего дня врача.
    /// </summary>
    public TimeOnly WorkDayEndTime { get; init; }

    /// <summary>
    /// Продолжительность обеда.
    /// </summary>
    public TimeSpan? LunchDuration { get; init; }

    /// <summary>
    /// Выходные дни, включённые в расчёт.
    /// </summary>
    public DayOfWeek[] IncludedWeekendDays { get; init; } = [];
}
