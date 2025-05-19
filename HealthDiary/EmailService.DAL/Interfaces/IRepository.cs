namespace EmailService.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
