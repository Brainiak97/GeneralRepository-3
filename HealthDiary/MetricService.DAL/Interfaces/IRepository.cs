namespace MetricService.DAL.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Получение списка объектов
        /// </summary>
        /// <returns>Список объектов</returns>
        public Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить объект по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Возвращает объект</returns>
        public Task<T>? GetByIdAsync(int id);

        /// <summary>
        /// создание объекта
        /// </summary>
        /// <param name="item"></param>
        public Task CreateAsync(T item);

        /// <summary>
        ///  обновление объекта
        /// </summary>
        /// <param name="item"></param>
        public Task UpdateAsync(T item);

        /// <summary>
        /// удаление объекта по id
        /// </summary>
        /// <param name="id"></param>
        public Task DeleteAsync(int id);


    }
}
