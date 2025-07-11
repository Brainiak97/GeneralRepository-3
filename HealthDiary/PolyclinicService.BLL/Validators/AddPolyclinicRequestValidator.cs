using FluentValidation;
using PolyclinicService.BLL.Common;
using PolyclinicService.BLL.Common.Extensions;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class AddPolyclinicRequestValidator : AbstractValidator<AddPolyclinicRequest>
{
    public AddPolyclinicRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Не заполнено наименование поликлиники");
        RuleFor(request => request.Address)
            .NotEmpty()
            .WithMessage("Не заполнен адрес поликлиники");
        RuleFor(request => request.PhoneNumber)
            .NotEmpty()
            .WithMessage("Не заполнен номер телефона")
            .PhoneNumber()
            .WithMessage("Некорректный формат номера телефона");
        RuleFor(request => request.Email)
            .EmailAddress()
            .WithMessage("Некорректный формат электронной почты")
            .When(request => request.Email is not null);
        RuleFor(request => request.Url)
            .UrlAddress()
            .WithMessage("Некорректный формат ссылки")
            .When(request => request.Url is not null);
    }
}