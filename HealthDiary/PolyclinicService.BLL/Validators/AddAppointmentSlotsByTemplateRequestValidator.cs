using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class AddAppointmentSlotsByTemplateRequestValidator : AbstractValidator<AddAppointmentSlotsByTemplateRequest>
{
    public AddAppointmentSlotsByTemplateRequestValidator()
    {
        RuleFor(r => r.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор поликлиники для слотов приёма в графике");
        RuleFor(r => r.DoctorIds)
            .NotEmpty()
            .WithMessage("")
            .ForEach(doctorId =>
                doctorId
                    .GreaterThan(0)
                    .WithMessage("Задан некорректный идентификатор врача"));
        RuleFor(r => r.PeriodStartDate)
            .GreaterThan(DateOnly.MinValue)
            .WithMessage("Не задана дата начала периода для формирования слотов")
            .LessThan(r => r.PeriodEndDate)
            .WithMessage("Дата начала должна быть раньше даты окончания периода для формирования слотов")
            .Custom((startDate, validationContext) =>
            {
                var requestProperties = validationContext.InstanceToValidate;
                if (startDate.Month != requestProperties.PeriodEndDate.Month ||
                    startDate.Year != requestProperties.PeriodEndDate.Year)
                {
                    validationContext.AddFailure(
                        "Период для формирования слотов должен быть в пределах одного календарного месяца");
                }
            });
        RuleFor(r => r.PeriodEndDate)
            .GreaterThan(DateOnly.MinValue)
            .WithMessage("Не задана дата окончания периода для формирования слотов")
            .GreaterThan(r => r.PeriodStartDate)
            .WithMessage("Дата окончания должна быть больше даты начала периода для формирования слотов");
        RuleFor(r => r.WorkDayStartTime)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Не задано время начала рабочего дня")
            .LessThan(r => r.WorkDayEndTime)
            .WithMessage("Время начала рабочего дня должно быть раньше окончания рабочего дня");
        RuleFor(r => r.WorkDayEndTime)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Не задано время окончания рабочего дня")
            .GreaterThan(r => r.WorkDayStartTime)
            .WithMessage("Время окончания рабочего дня должно быть позднее начала рабочего дня");
        RuleFor(r => r.AppointmentDuration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Не задана продолжительность приёма")
            .Custom((duration, validationContext) =>
            {
                var requestProperties = validationContext.InstanceToValidate;
                var workDuration = requestProperties.WorkDayEndTime - requestProperties.WorkDayStartTime - (requestProperties.LunchDuration ?? TimeSpan.Zero);
                if (workDuration / duration is not 0)
                {
                    validationContext.AddFailure(
                        "Общая продолжительность рабочего времени в день должны быть кратно продолжительности приёма");
                }
            });
        RuleFor(r => r.LunchDuration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Задана некорректная продолжительность обеда")
            .When(r => r.LunchDuration is not null);
        RuleFor(r => r.IncludedWeekendDays)
            .IsInEnum()
            .ForEach(wd =>
                wd.Custom((day, validationContext) =>
                {
                    var weekendDay = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
                    if (!weekendDay.Contains(day))
                    {
                        validationContext.AddFailure($"{day} не является выходным днём");    
                    }
                }))
            .WithMessage("Заданы некорректные выходные дни, которые нужно включить в график")
            .When(r => r.IncludedWeekendDays.Length > 0);
    }
}