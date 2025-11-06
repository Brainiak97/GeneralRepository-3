using FluentValidation;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Validators;

internal class UserSlotReservationRequestValidator : AbstractValidator<UserSlotReservationRequest>
{
    public UserSlotReservationRequestValidator()
    {
        RuleFor(r => r.SlotId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор слота приёма в графике");
        RuleFor(r => r.UserId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор пользователя, который резервирует слот.");
    }
}