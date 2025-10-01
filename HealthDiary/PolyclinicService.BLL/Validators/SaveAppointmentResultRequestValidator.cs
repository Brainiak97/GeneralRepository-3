using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class SaveAppointmentResultRequestValidator : AbstractValidator<SaveAppointmentResultRequest>
{
    public SaveAppointmentResultRequestValidator()
    {
        RuleFor(x => x.AppointmentSlotId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор слота приёма в графике");
        RuleFor(r => r.ReportTemplateId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор шаблона отчёта");
        RuleFor(r => r.ReportContent)
            .NotEmpty()
            .WithMessage("Задан пустой отчёт");
    }    
}