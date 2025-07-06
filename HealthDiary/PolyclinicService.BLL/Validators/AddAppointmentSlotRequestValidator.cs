using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class AddAppointmentSlotRequestValidator : AbstractValidator<AddAppoinmentSlotRequest>
{
    public AddAppointmentSlotRequestValidator()
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
            .GreaterThan(DateOnly.MinValue)
            .WithMessage("Не задана дата приёма слота в графике");
        RuleFor(r => r.StartTime)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Не задано время начала приёма для слота в графике");
        RuleFor(r => r.EndTime)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Не задано время окончания приёма для слота в графике");
    }
}