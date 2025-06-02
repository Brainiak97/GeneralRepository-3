namespace EmailService.DAL.Interfaces
{
    /// <summary>
    /// Определяет общие методы доступа к данным для работы с сущностями.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий. Должен быть ссылочным типом.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Асинхронно получает сущность по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденную сущность или <see langword="null"/>, если сущность не найдена.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Асинхронно получает все сущности типа <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает коллекцию всех сущностей или <see langword="null"/>.</returns>
        Task<IEnumerable<T>?> GetAllAsync();

        /// <summary>
        /// Асинхронно добавляет новую сущность в хранилище.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает добавленную сущность или <see langword="null"/>.</returns>
        Task<T?> AddAsync(T entity);

        /// <summary>
        /// Асинхронно обновляет существующую сущность.
        /// </summary>
        /// <param name="entity">Обновлённая сущность.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает обновлённую сущность или <see langword="null"/>.</returns>
        Task<T?> UpdateAsync(T entity);

        /// <summary>
        /// Асинхронно удаляет указанную сущность из хранилища.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task DeleteAsync(T entity);
    }
}