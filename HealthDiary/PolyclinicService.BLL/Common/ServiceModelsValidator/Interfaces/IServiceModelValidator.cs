namespace PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

/// <summary>
/// Валидатор моделей данных сервиса.
/// </summary>
internal interface IServiceModelValidator
{
    /// <summary>
    /// Провалидировать модель и бросить исключение при нахождении ошибки.
    /// </summary>
    /// <param name="model">Модель данных сервиса.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task ValidateAndThrowAsync<TModel>(TModel model)
        where TModel : class;

    /// <summary>
    /// Провалидировать модель.
    /// </summary>
    /// <param name="model">Модель данных сервиса.</param>
    /// <returns>Результат валидации модели.</returns>
    Task<IValidationResult> ValidateAsync<TModel>(TModel model)
        where TModel : class;
}