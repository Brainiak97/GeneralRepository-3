using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PolyclinicService.BLL.Common.Exceptions;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

namespace PolyclinicService.BLL.Common.ServiceModelsValidator;

/// <inheritdoc />
internal class ValidatorsProvider(IServiceProvider serviceProvider) : IValidatorsProvider
{
    /// <inheritdoc />
    /// <exception cref="ValidatorNotFoundException">Если валидатор для типа <typeparamref name="TValidationObject"/> не зарегистрирован в DI-контейнере.</exception>
    public IValidator<TValidationObject> GetRequiredValidator<TValidationObject>(TValidationObject obj)
        where TValidationObject : class => serviceProvider.GetService<IValidator<TValidationObject>>()
                                           ?? throw new ValidatorNotFoundException($"Не найден валидатор для модели типа {obj.GetType().Name}");
}