namespace MetricService.DAL.Interfaces
{
    public interface IReadRepository<T>
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
        public Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Имя набора данных
        /// </summary>
        public string Name {  get; }
    }
}
