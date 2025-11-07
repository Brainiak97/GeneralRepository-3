using FluentValidation;
using PolyclinicService.BLL.Data.Commands;

namespace PolyclinicService.BLL.Validators;

internal class GetPatientAppointmentSlotsCommandValidator : AbstractValidator<GetPatientAppointmentSlotsCommand>
{
    public GetPatientAppointmentSlotsCommandValidator()
    {
        RuleFor(m => m.PatientId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор пациента (пользователя)");
        RuleFor(m => m.StartDate)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Задана некорректная дата с которой необходимо получить слоты")
            .When(m => m.StartDate is not null)
            .LessThanOrEqualTo(m => m.EndDate)
            .WithMessage("Начальная дата должна быть меньше или равна дате окончания периода, на которую необходимо получить слоты")
            .When(m => m.EndDate is not null && m.StartDate is not null);
        RuleFor(m => m.EndDate)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Задана некорректная дата по которую необходимо получить слоты")
            .When(m => m.EndDate is not null);
    }
}