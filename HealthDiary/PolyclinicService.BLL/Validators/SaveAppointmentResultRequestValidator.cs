using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class SaveAppointmentResultRequestValidator : AbstractValidator<SaveAppointmentResultRequest>
{
    public SaveAppointmentResultRequestValidator()
    {
        RuleFor(r => r.AppointmentSlotId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор результата приёма");
        RuleFor(r => r.ReportTemplateId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор шаблона отчёта");
        RuleFor(r => r.ReportContent)
            .NotEmpty()
            .WithMessage("Задан пустой отчёт");
    }    
}