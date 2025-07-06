using FluentValidation;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using IValidatorFactory = PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces.IValidatorFactory;

namespace PolyclinicService.BLL.Common.ServiceModelsValidator;

/// <inheritdoc />
internal class ServiceModelValidator(IValidatorFactory validatorFactory) : IServiceModelValidator
{
    /// <inheritdoc />
    public async Task ValidateAndThrowAsync<TModel>(TModel model)
        where TModel : class
    {
        ArgumentNullException.ThrowIfNull(model);

        var validator = validatorFactory.GetRequiredValidator(model);
        await validator.ValidateAndThrowAsync(model);
    }

    public async Task<IValidationResult> ValidateAsync<TModel>(TModel model)
        where TModel : class
    {
        ArgumentNullException.ThrowIfNull(model);

        var validator = validatorFactory.GetRequiredValidator(model);
        var validationResult = await validator.ValidateAsync(model);

        return new ValidationResult
        {
            IsValid = validationResult.IsValid,
            ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList(),
        };
    }
}