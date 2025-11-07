using FluentValidation;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

namespace PolyclinicService.BLL.Common.ServiceModelsValidator;

/// <inheritdoc />
internal class ServiceModelValidator(IValidatorsProvider validatorsProvider) : IServiceModelValidator
{
    /// <inheritdoc />
    public async Task ValidateAndThrowAsync<TModel>(TModel model)
        where TModel : class
    {
        ArgumentNullException.ThrowIfNull(model);

        var validator = validatorsProvider.GetRequiredValidator(model);
        await validator.ValidateAndThrowAsync(model);
    }

    public async Task<IValidationResult> ValidateAsync<TModel>(TModel model)
        where TModel : class
    {
        ArgumentNullException.ThrowIfNull(model);

        var validator = validatorsProvider.GetRequiredValidator(model);
        var validationResult = await validator.ValidateAsync(model);

        return new ValidationResult
        {
            IsValid = validationResult.IsValid,
            ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList(),
        };
    }
}