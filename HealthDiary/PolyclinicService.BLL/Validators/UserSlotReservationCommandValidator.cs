using FluentValidation;
using PolyclinicService.BLL.Data.Commands;

namespace PolyclinicService.BLL.Validators;

internal class UserSlotReservationCommandValidator : AbstractValidator<UserSlotReservationCommand>
{
    public UserSlotReservationCommandValidator()
    {
        RuleFor(r => r.SlotId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор слота приёма в графике");
        RuleFor(r => r.UserId)
            .GreaterThan(0)
            .WithMessage("Не задан идентификатор пользователя, который резервирует слот.");
    }
}