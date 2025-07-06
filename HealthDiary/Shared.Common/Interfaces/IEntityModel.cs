namespace Shared.Common.Interfaces;

/// <summary>
/// Модель доменной сущности.
/// </summary>
public interface IEntityModel<TType>
{
    /// <summary>
    /// Идентификатор доменной сущности.
    /// </summary>
    public TType Id { get; set; }
}