using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class AddDoctorRequestValidator : AbstractValidator<AddDoctorRequest>
{
    public AddDoctorRequestValidator()
    {
        RuleFor(r => r.UserId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор врача");
        RuleFor(r => r.Seniority)
            .GreaterThanOrEqualTo(v => 0)
            .WithMessage("Не задан стаж врача");
        RuleFor(r => r.QualificationType)
            .IsInEnum()
            .WithMessage("Не задана квалификация врача");
        RuleFor(r => r.AcademyDegree)
            .IsInEnum()
            .WithMessage("Не задана научная степень врача")
            .When(r => r.AcademyDegree is not null);
        RuleFor(r => r.SpecializationType)
            .IsInEnum()
            .WithMessage("Не задана специализация врача");
    }
}