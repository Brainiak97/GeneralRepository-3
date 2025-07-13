using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;

namespace PolyclinicService.BLL.Common.ServiceModelsValidator;

/// <inheritdoc />
public record ValidationResult : IValidationResult
{
    /// <inheritdoc />
    public bool IsValid { get; init; }

    /// <inheritdoc />
    public ICollection<string> ValidationErrors { get; init; } = Array.Empty<string>();
}