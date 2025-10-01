using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class UpdateAppointmentResultRequestValidator : AbstractValidator<UpdateAppointmentResultRequest>
{
    public UpdateAppointmentResultRequestValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор результата приёма");
        RuleFor(r => r.ReportTemplateId)
            .GreaterThan(0)
            .WithMessage("Задан некорректный идентификатор шаблона отчёта")
            .When(r => r.ReportTemplateId is not null)
            .Custom((_, validationContext) =>
            {
                var instance = validationContext.InstanceToValidate;
                if (instance.ReportTemplateId is not null && instance.ReportContent is null)
                {
                    validationContext.AddFailure("При изменении типа шаблона, должен быть новый отчёт");
                }
            });
        RuleFor(r => r.ReportContent)
            .NotEmpty()
            .WithMessage("Отредактированный отчёт не должен быть пустой")
            .When(r => r.ReportContent is not null);
    }    
}