namespace Shared.Common.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория.
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности.</typeparam>
/// <typeparam name="TKey">Тип идентификатора доменной сущности.</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : IEntityModel<TKey>
{
    /// <summary>
    /// Асинхронно получает сущность по указанному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сущности.</param>
    /// <returns>Задача, представляющая асинхронную операцию. 
    /// Возвращает найденную сущность или <see langword="null"/>, если сущность не найдена.</returns>
    Task<TEntity?> GetByIdAsync(TKey id);

    /// <summary>
    /// Асинхронно создаёт новую сущность в базе данных.
    /// </summary>
    /// <param name="entity">Добавляемая сущность.</param>
    /// <returns>Задача, представляющая асинхронную операцию. 
    /// Возвращает добавленную сущность или <see langword="null"/>.</returns>
    Task<TKey> AddAsync(TEntity entity);

    /// <summary>
    /// Асинхронно обновляет существующую сущность.
    /// </summary>
    /// <param name="entity">Обновлённая сущность.</param>
    /// <returns>Задача, представляющая асинхронную операцию. 
    /// Возвращает обновлённую сущность или <see langword="null"/>.</returns>
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// Асинхронно удаляет указанную сущность из базы данных.
    /// </summary>
    /// <param name="id">Идентификатор сущности для удаления.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task DeleteAsync(TKey id);
}