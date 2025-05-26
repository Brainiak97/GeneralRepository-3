namespace MetricService.DAL.Interfaces
{
    public interface IWriteRepository<T>: IReadRepository<T>
    {
        /// <summary>
        /// создание объекта
        /// </summary>
        /// <param name="item"></param>
        public Task<bool> CreateAsync(T item);

        /// <summary>
        ///  обновление объекта
        /// </summary>
        /// <param name="item"></param>
        public Task<bool> UpdateAsync(T item);

        /// <summary>
        /// удаление объекта по id
        /// </summary>
        /// <param name="id"></param>
        public Task<bool> DeleteAsync(int id);
    }
}
