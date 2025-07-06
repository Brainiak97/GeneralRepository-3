using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class DoctorActiveAppointmentSlotsRequestValidator : AbstractValidator<DoctorActiveAppointmentSlotsRequest>
{
    public DoctorActiveAppointmentSlotsRequestValidator()
    {
        RuleFor(r => r.PolyclinicId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор поликлиники");
        RuleFor(r => r.DoctorId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор врача");
        RuleFor(r => r.Date)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Задана некорректная дата, на которую необходимо получить слоты приёма врача")
            .When(r => r.Date.HasValue);
    }
}