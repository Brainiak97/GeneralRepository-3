using FluentValidation;
using PolyclinicService.BLL.Common;
using PolyclinicService.BLL.Common.Extensions;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class UpdatePolyclinicRequestValidator : AbstractValidator<UpdatePolyclinicRequest>
{
    public UpdatePolyclinicRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Не заполнено наименование поликлиники")
            .When(request => request.Name is not null);
        RuleFor(request => request.Address)
            .NotEmpty()
            .WithMessage("Задан некорректный новый адрес поликлиники")
            .When(request => request.Address is not null);
        RuleFor(request => request.PhoneNumber)
            .PhoneNumber()
            .WithMessage("Задан некорректный формат нового номера телефона")
            .When(request => request.PhoneNumber is not null);
        RuleFor(request => request.Email)
            .EmailAddress()
            .WithMessage("Задан некорректный формат новой электронной почты")
            .When(request => request.Email is not null);
        RuleFor(request => request.Url)
            .UrlAddress()
            .WithMessage("Задан некорректный формат новой ссылки на сайт поликлиники")
            .When(request => request.Url is not null);
    }
}