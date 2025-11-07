using FluentValidation;
using PolyclinicService.BLL.Data.Commands;

namespace PolyclinicService.BLL.Validators;

internal class PolyclinicAppointmentSlotsByDateCommandValidator : AbstractValidator<PolyclinicAppointmentSlotsByDateCommand>
{
    public PolyclinicAppointmentSlotsByDateCommandValidator()
    {
        RuleFor(r => r.Date)
            .GreaterThan(DateTime.MinValue)
            .Equal(r => r.Date.Date)
            .WithMessage("Задана некорректная дата, на которую необходимо получить слоты приёма");
        RuleFor(r => r.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор поликлиники");
    }
}