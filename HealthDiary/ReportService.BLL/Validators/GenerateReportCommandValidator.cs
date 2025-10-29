using FluentValidation;
using ReportService.BLL.Common;
using ReportService.BLL.Data.Commands;

namespace ReportService.BLL.Validators;

internal class GenerateReportCommandValidator : AbstractValidator<GenerateReportCommand>
{
    public GenerateReportCommandValidator()
    {
        RuleFor(r => r.EntityId)
            .GreaterThan(0)
            .WithMessage(ValidationExceptionMessages.InvalidEntityIdMessage);
        RuleFor(r => r.ReportContent)
            .NotEmpty()
            .WithMessage(ValidationExceptionMessages.ReportContentIsEmptyMessage);
        RuleFor(r => r.TemplateId)
            .GreaterThan(0)
            .WithMessage(ValidationExceptionMessages.InvalidTemplateIdMessage);
    }
}