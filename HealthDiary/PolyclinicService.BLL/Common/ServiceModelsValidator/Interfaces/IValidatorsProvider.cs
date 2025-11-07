using FluentValidation;

namespace PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

/// <summary>
/// Провайдер для работы с валидаторами сущностей бизнес-логики.
/// </summary>
internal interface IValidatorsProvider
{
    /// <summary>
    /// Создать валидатор, зарегистрированный в DI, соответствующий типу валидируемой сущности.
    /// </summary>
    /// <param name="obj">Валидируемый объект.</param>
    /// <typeparam name="TValidationObject">Тип сущности, который необходимо валидировать.</typeparam>
    /// <returns>Валидатор сущности.</returns>
    IValidator<TValidationObject> GetRequiredValidator<TValidationObject>(TValidationObject obj)
        where TValidationObject : class;
}