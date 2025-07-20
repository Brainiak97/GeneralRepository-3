using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class UpdateDoctorRequestValidator : AbstractValidator<UpdateDoctorRequest>
{
    public UpdateDoctorRequestValidator()
    {
        RuleFor(r => r.DoctorId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор врача");
        RuleFor(r => r.Seniority)
            .GreaterThanOrEqualTo(v => 0)
            .WithMessage("Не задан стаж врача")
            .When(r => r.Seniority is not null);
        RuleFor(r => r.QualificationType)
            .IsInEnum()
            .WithMessage("Не задана квалификация врача")
            .When(r => r.QualificationType is not null);
        RuleFor(r => r.AcademyDegree)
            .IsInEnum()
            .WithMessage("Не задана научная степень врача")
            .When(r => r.AcademyDegree is not null);
        RuleFor(r => r.SpecializationType)
            .IsInEnum()
            .WithMessage("Не задана специализация врача")
            .When(r => r.SpecializationType is not null);
    }
}
