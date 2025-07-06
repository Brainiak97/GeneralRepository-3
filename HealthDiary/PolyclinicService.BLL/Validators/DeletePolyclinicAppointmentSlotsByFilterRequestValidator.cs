using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class DeletePolyclinicAppointmentSlotsByFilterRequestValidator : AbstractValidator<DeletePolyclinicAppointmentSlotsByFilterRequest>
{
    public DeletePolyclinicAppointmentSlotsByFilterRequestValidator()
    {
        RuleFor(r => r.DoctorId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор врача")
            .When(r => r.DoctorId is not null);
        RuleFor(r => r.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор поликлиники")
            .When(r => r.PolyclinicId is not null);
        RuleFor(r => r.PeriodStartDate)
            .GreaterThan(DateOnly.MinValue)
            .WithMessage("Задана некорректная дата начала временного интервала для удаления")
            .When(r => r.PeriodStartDate is not null)
            .LessThanOrEqualTo(r => r.PeriodEndDate)
            .WithMessage("Дата начала временного интервала для удаления должна быть меньше или равна дате окончания этого периода")
            .When(r => r.PeriodStartDate is not null && r.PeriodEndDate is not null);
        RuleFor(r => r.PeriodEndDate)
            .GreaterThan(DateOnly.MinValue)
            .WithMessage("Задана некорректная дата окончания временного интервала для удаления")
            .When(r => r.PeriodStartDate is not null)
            .GreaterThanOrEqualTo(r => r.PeriodStartDate)
            .WithMessage("Дата окончания временного интервала для удаления должна быть больше или равна дате начала этого периода")
            .When(r => r.PeriodStartDate is not null && r.PeriodEndDate is not null);   
    }
}