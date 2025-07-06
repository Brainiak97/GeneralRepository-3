using FluentValidation;

namespace PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

/// <summary>
/// Фабрика валидаторов сущностей бизнес-логики.
/// </summary>
internal interface IValidatorFactory
{
    /// <summary>
    /// Вернуть валидатор, соотвествующий типу сущности.
    /// </summary>
    /// <param name="obj">Валидируемый объект,</param>
    /// <typeparam name="TValidationObject">Тип сущности, который необходимо валидировать.</typeparam>
    /// <returns>Валидатор сущности.</returns>
    IValidator<TValidationObject> GetRequiredValidator<TValidationObject>(TValidationObject obj)
        where TValidationObject : class;
}