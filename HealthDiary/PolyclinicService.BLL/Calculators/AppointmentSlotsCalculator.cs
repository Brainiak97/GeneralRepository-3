using PolyclinicService.BLL.Data;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.Domain.Models;

namespace PolyclinicService.BLL.Calculators;

/// <inheritdoc />
internal class AppointmentSlotsCalculator : IAppointmentSlotsCalculator
{
    /// <inheritdoc />
    public IEnumerable<AppointmentSlotDto> CalculateSlots(AppointmentSlotsCalculationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var dayWorkPeriods = GetDayWorkPeriods(
            context.WorkDayStartTime,
            context.WorkDayEndTime,
            context.LunchDuration,
            context.AppointmentDuration);

        var slots = new List<AppointmentSlotDto>();
        for (var date = context.PeriodStartDate; date <= context.PeriodEndDate; date = date.AddDays(1))
        {
            if (IsWeekend(date) && !context.IncludedWeekendDays.Contains(date.DayOfWeek))
            {
                continue;
            }

            var daySlots = CalculateSlotsOnDay(
                context.PolyclinicId,
                context.DoctorIds,
                date,
                dayWorkPeriods,
                context.AppointmentDuration);

            slots.AddRange(daySlots);
        }

        return slots;
    }

    private (TimeOnly StartTime, TimeOnly EndTime)[] GetDayWorkPeriods(
        TimeOnly workStartTime,
        TimeOnly workEndTime,
        TimeSpan? lunchDuration,
        TimeSpan appointmentDuration)
    {
        if (!lunchDuration.HasValue)
        {
            return [new ValueTuple<TimeOnly, TimeOnly>(workStartTime, workEndTime)];
        }

        var startLunchTime = CalculateLunchStartTime(
            workStartTime,
            workEndTime,
            lunchDuration.Value,
            appointmentDuration);

        return [
            new ValueTuple<TimeOnly, TimeOnly>(workStartTime, startLunchTime),
            new ValueTuple<TimeOnly, TimeOnly>(startLunchTime.Add(lunchDuration.Value), workEndTime)
        ];
    }

    private static IList<AppointmentSlotDto> CalculateSlotsOnDay(
        int polyclinicId,
        int[] doctorIds,
        DateOnly date,
        (TimeOnly StartTime, TimeOnly EndTime)[] workDayPeriods,
        TimeSpan appointmentDuration)
    {
        var daySlots = new List<AppointmentSlotDto>();
        foreach (var workPeriod in workDayPeriods)
        {
            var slotStartTime = workPeriod.StartTime;

            while (slotStartTime < workPeriod.EndTime)
            {
                daySlots.AddRange(
                    doctorIds.Select(doctorId =>
                        new AppointmentSlotDto
                        {
                            PolyclinicId = polyclinicId,
                            DoctorId = doctorId,
                            Status = AppointmentSlotStatus.Created,
                            Date = new DateTime(date, slotStartTime, DateTimeKind.Local),
                            Duration = appointmentDuration,
                        }));

                slotStartTime = slotStartTime.Add(appointmentDuration);
            }
        }

        return daySlots;
    }

    private TimeOnly CalculateLunchStartTime(
        TimeOnly workStartTime,
        TimeOnly workEndTime,
        TimeSpan lunchDuration,
        TimeSpan appointmentDuration)
    {
        var allowedWorkTime = new TimeSpan(workEndTime.Ticks - workStartTime.Ticks - lunchDuration.Ticks);
        if (allowedWorkTime.Minutes % appointmentDuration.Minutes != 0)
        {
            throw new InvalidOperationException("Общая продолжительность рабочего времени в день должна быть кратна продолжительности приёма");
        }

        var approximateSlotsBeforeLunch = (allowedWorkTime / appointmentDuration) / 2.0;
        var slotsBeforeLunch = (int)(approximateSlotsBeforeLunch - Math.Floor(approximateSlotsBeforeLunch) > 0.5
            ? Math.Ceiling(approximateSlotsBeforeLunch)
            : Math.Floor(approximateSlotsBeforeLunch));

        return workStartTime.Add(TimeSpan.FromTicks(slotsBeforeLunch * appointmentDuration.Ticks));
    }

    private static bool IsWeekend(DateOnly date) =>
        date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}