using FluentValidation;
using ReportService.BLL.Common;
using ReportService.BLL.Data.Commands;

namespace ReportService.BLL.Validators;

internal class ServiceCommandValidator : AbstractValidator<IServiceCommand>
{
    public ServiceCommandValidator()
    {
        RuleFor(r => r.ReportId)
            .GreaterThan(0)
            .WithMessage(ValidationExceptionMessages.InvalidReportIdMessage);
        RuleFor(r => r.FileName)
            .NotEmpty()
            .WithMessage(ValidationExceptionMessages.InvalidFileNameMessage);
        RuleFor(r => r.Content)
            .NotEmpty()
            .WithMessage(ValidationExceptionMessages.ReportContentIsEmptyMessage);
    }
}