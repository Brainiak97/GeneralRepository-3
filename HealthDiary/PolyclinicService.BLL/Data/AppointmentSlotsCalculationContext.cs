namespace PolyclinicService.BLL.Data;

/// <summary>
/// Контекст расчёта слотов приёмов к врачу.
/// </summary>
/// <param name="PolyclinicId">Идентификатор поликлиники.</param>
/// <param name="DoctorIds">Идентификатор врачей, для которых добавляются слоты.</param>
/// <param name="PeriodStartDate">Дата начала периода для формирования слотов.</param>
/// <param name="PeriodEndDate">Дата окончания периода для формирования слотов.</param>
/// <param name="AppointmentDuration">Продолжительность приёма.</param>
/// <param name="WorkDayStartTime">Время начала рабочего дня врача.</param>
/// <param name="WorkDayEndTime">Время окончания рабочего дня врача.</param>
/// <param name="LunchDuration">Продолжительность обеда.</param>
/// <param name="IncludedWeekendDays">Выходные дни, включённые в расчёт.</param>
internal record AppointmentSlotsCalculationContext(
    int PolyclinicId,
    int[] DoctorIds,
    DateOnly PeriodStartDate,
    DateOnly PeriodEndDate,
    TimeSpan AppointmentDuration,
    TimeOnly WorkDayStartTime,
    TimeOnly WorkDayEndTime,
    TimeSpan? LunchDuration,
    DayOfWeek[] IncludedWeekendDays);