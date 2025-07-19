using FluentValidation;

namespace PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

/// <summary>
/// Фабрика валидаторов сущностей бизнес-логики.
/// </summary>
internal interface IValidatorFactory
{
    /// <summary>
    /// Создать валидатор, зарегистрированный в DI, соответствующий типу валидируемой сущности.
    /// </summary>
    /// <param name="obj">Валидируемый объект.</param>
    /// <typeparam name="TValidationObject">Тип сущности, который необходимо валидировать.</typeparam>
    /// <returns>Валидатор сущности.</returns>
    IValidator<TValidationObject> CreateRequiredValidator<TValidationObject>(TValidationObject obj)
        where TValidationObject : class;
}