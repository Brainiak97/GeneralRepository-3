using FluentValidation;
using PolyclinicService.BLL.Data.Commands;

namespace PolyclinicService.BLL.Validators;

internal class UpdateAppointmentSlotCommandValidator : AbstractValidator<UpdateAppointmentSlotCommand>
{
    public UpdateAppointmentSlotCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор приёма в графике поликлиники");
        RuleFor(x => x.DoctorId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор врача для изменения слота приёма");
        RuleFor(x => x.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор поликлиники для изменения слота приёма");
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный новый идентификатор пациента для изменения слота приёма")
            .When(x => x.UserId is not null);
        RuleFor(x => x.Date)
            .NotEqual(default(DateTime))
            .WithMessage("Задана некорректная новая дата приёма в слоте приёма графика");
        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Задано некорректное новое время начала приёма в слоте графика");
    }
}