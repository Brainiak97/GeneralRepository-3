namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, определяющий основные операции взаимодействия с базой данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Создать объект
        /// </summary>
        /// <param name="item"></param>
        public Task<bool> CreateAsync(T item);

        /// <summary>
        ///  Обновить объект
        /// </summary>
        /// <param name="item"></param>
        public Task<bool> UpdateAsync(T item);

        /// <summary>
        /// Удалить объект
        /// </summary>
        /// <param name="id">Идентификатор объекта в БД</param>
        public Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Получить список объектов
        /// </summary>
        /// <returns>Список объектов</returns>
        public Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить объект
        /// </summary>
        /// <param name="id">Идентификатор объекта в БД</param>
        /// <returns>Объект</returns>
        public Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Возвращает имя набора данных
        /// </summary>
        public string Name { get; }
    }
}
