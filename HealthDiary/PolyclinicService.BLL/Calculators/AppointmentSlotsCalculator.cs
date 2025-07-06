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

    private (TimeSpan StartTime, TimeSpan EndTime)[] GetDayWorkPeriods(
        TimeSpan workStartTime,
        TimeSpan workEndTime,
        TimeSpan? lunchDuration,
        TimeSpan appointmentDuration)
    {
        if (!lunchDuration.HasValue)
        {
            return [new ValueTuple<TimeSpan, TimeSpan>(workStartTime, workEndTime)];
        }

        var startLunchTime = CalculateLunchStartTime(
            workStartTime,
            workEndTime,
            lunchDuration.Value,
            appointmentDuration);

        return [
            new ValueTuple<TimeSpan, TimeSpan>(workStartTime, startLunchTime),
            new ValueTuple<TimeSpan, TimeSpan>(startLunchTime + lunchDuration.Value, workEndTime)
        ];
    }

    private static IList<AppointmentSlotDto> CalculateSlotsOnDay(
        int polyclinicId,
        int[] doctorIds,
        DateOnly date,
        (TimeSpan StartTime, TimeSpan EndTime)[] workDayPeriods,
        TimeSpan appointmentDuration)
    {
        var daySlots = new List<AppointmentSlotDto>();
        foreach (var workPeriod in workDayPeriods)
        {
            var slotStartTime = workPeriod.StartTime;
            var slotEndTime = slotStartTime.Add(appointmentDuration);

            while (slotStartTime != workPeriod.EndTime)
            {
                daySlots.AddRange(
                    doctorIds.Select(doctorId =>
                        new AppointmentSlotDto
                        {
                            PolyclinicId = polyclinicId,
                            DoctorId = doctorId,
                            Status = AppointmentSlotStatus.Created,
                            StartTime = slotStartTime,
                            EndTime = slotEndTime,
                            Date = date
                        }));

                slotStartTime = slotEndTime;
            }
        }

        return daySlots;
    }

    private TimeSpan CalculateLunchStartTime(
        TimeSpan workStartTime,
        TimeSpan workEndTime,
        TimeSpan lunchDuration,
        TimeSpan appointmentDuration)
    {
        var allowedWorkTime = workEndTime - lunchDuration - workStartTime;
        if (allowedWorkTime.Minutes % appointmentDuration.Minutes != 0)
        {
            throw new InvalidOperationException("Общая продолжительность рабочего времени в день должны быть кратно продолжительности приёма");
        }

        var slotsCount = allowedWorkTime.Minutes / appointmentDuration.Minutes;
        var slotsBeforeLunch = slotsCount / 2.0;

        slotsBeforeLunch = (int)(slotsBeforeLunch - Math.Floor(slotsBeforeLunch) > 0.5
            ? Math.Ceiling(slotsBeforeLunch)
            : Math.Floor(slotsBeforeLunch));

        return TimeSpan.FromMinutes(workStartTime.Minutes + slotsBeforeLunch * appointmentDuration.Minutes);
    }

    private static bool IsWeekend(DateOnly date) =>
        date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}