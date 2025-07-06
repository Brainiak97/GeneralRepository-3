using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о физической активности
    /// </summary>
    /// <seealso cref="PhysicalActivity" />
    public interface IPhysicalActivityRepository : IRepository<PhysicalActivity>
    {
        /// <summary>
        /// Получить список записей из справочника по строке поиска.
        /// Разные фразы для поиска разделяются ","
        /// </summary>
        /// <param name="search">строка поиска. Для разделения фраз использовать ","</param>
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<PhysicalActivity>> GetListPhysicalActivitiesBySearchAsync(string search);
    }
}
