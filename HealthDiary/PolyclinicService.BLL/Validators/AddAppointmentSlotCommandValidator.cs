using FluentValidation;
using PolyclinicService.BLL.Data.Commands;

namespace PolyclinicService.BLL.Validators;

internal class AddAppointmentSlotCommandValidator : AbstractValidator<AddAppoinmentSlotCommand>
{
    public AddAppointmentSlotCommandValidator()
    {
        RuleFor(r => r.DoctorId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор врача для слота приёма в графике");
        RuleFor(r => r.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор поликлиники для слота приёма в графике");
        RuleFor(r => r.UserId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор пациента для слота приёма в графике")
            .When(r => r.UserId is not null);
        RuleFor(r => r.Date)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Не задана дата приёма слота в графике");
        RuleFor(r => r.Duration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Не задана продолжительность приёма врача");
    }
}