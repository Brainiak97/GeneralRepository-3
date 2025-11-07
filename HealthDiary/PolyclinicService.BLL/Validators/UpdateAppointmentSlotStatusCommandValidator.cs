using FluentValidation;
using PolyclinicService.BLL.Data.Commands;

namespace PolyclinicService.BLL.Validators;

internal class UpdateAppointmentSlotStatusCommandValidator : AbstractValidator<UpdateAppointmentSlotStatusCommand>
{
    public UpdateAppointmentSlotStatusCommandValidator()
    {
        RuleFor(x => x.AppointmentSlotId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор слота приёма к врачу");
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Не задан новый статус слота приёма к врачу");
    }
}