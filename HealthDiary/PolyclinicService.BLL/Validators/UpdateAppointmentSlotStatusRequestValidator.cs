using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class UpdateAppointmentSlotStatusRequestValidator : AbstractValidator<UpdateAppointmentSlotStatusRequest>
{
    public UpdateAppointmentSlotStatusRequestValidator()
    {
        RuleFor(x => x.AppointmentSlotId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор слота приёма к врачу");
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Не задан новый статус слота приёма к врачу");
    }
}