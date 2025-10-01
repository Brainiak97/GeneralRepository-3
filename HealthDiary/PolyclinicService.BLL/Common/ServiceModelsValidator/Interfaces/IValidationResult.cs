namespace PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

/// <summary>
/// Интерфейс для результата валидации.
/// </summary>
public interface IValidationResult
{
    /// <summary>
    /// Признак успешного прохождения валидации.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Список ошибок.
    /// </summary>
    public ICollection<string> ValidationErrors { get; }
}