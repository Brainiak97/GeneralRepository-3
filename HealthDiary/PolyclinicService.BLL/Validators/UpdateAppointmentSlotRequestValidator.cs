using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class UpdateAppointmentSlotRequestValidator : AbstractValidator<UpdateAppointmentSlotRequest>
{
    public UpdateAppointmentSlotRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор приёма в графике поликлиники");
        RuleFor(x => x.DoctorId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор врача для изменения слота приёма")
            .When(x => x.DoctorId is not null);
        RuleFor(x => x.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор поликлиники для изменения слота приёма")
            .When(x => x.PolyclinicId is not null);
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор пациента для изменения слота приёма")
            .When(x => x.UserId is not null);
        RuleFor(x => x.Date)
            .NotEqual((DateOnly)default)
            .WithMessage("Задана некорректная новая дата приёма в слоте приёма графика")
            .When(x => x.Date is not null);
        RuleFor(x => x.StartTime)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Задано некорректное новое время начала приёма в слоте графика")
            .When(x => x.StartTime is not null)
            .LessThan(x => x.EndTime)
            .WithMessage("Новое время начала приёма не должно быть позднее новой даты окончания приёма")
            .When(x => x.StartTime is not null && x.EndTime is not null);
        RuleFor(x => x.EndTime)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Задано некорректное новое время окончания приёма в слоте графика")
            .When(x => x.EndTime is not null)
            .GreaterThan(x => x.StartTime)
            .WithMessage("Новое время окончания приёма не должно быть раньше новой даты начала приёма")
            .When(x => x.EndTime is not null && x.StartTime is not null);
    }
}