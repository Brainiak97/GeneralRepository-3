using FluentValidation;
using PolyclinicService.BLL.Data.Commands;

namespace PolyclinicService.BLL.Validators;

internal class DoctorActiveAppointmentSlotsCommandValidator : AbstractValidator<DoctorActiveAppointmentSlotsCommand>
{
    public DoctorActiveAppointmentSlotsCommandValidator()
    {
        RuleFor(r => r.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор поликлиники");
        RuleFor(r => r.DoctorId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор врача");
        RuleFor(r => r.Date)
            .GreaterThan(DateTime.MinValue)
            .Equal(r => r.Date!.Value.Date)
            .WithMessage("Задана некорректная дата, на которую необходимо получить слоты приёма врача")
            .When(r => r.Date.HasValue);
    }
}