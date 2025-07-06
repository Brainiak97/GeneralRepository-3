using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о категории анализов
    /// </summary>
    /// <seealso cref="AnalysisCategory" />
    public interface IAnalysisCategoryRepository: IRepository<AnalysisCategory>
    {
        /// <summary>
        /// Получить список записей из справочника "Категории анализов", заданной строкой поиска по наименованию и описанию.
        /// В строку поиска можно передать несколько фраз поиска через ","
        /// </summary>
        /// <param name="search">Cтрока поиска по наименованию</param>
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<AnalysisCategory>> GetListAnalysisCategoriesBySearchAsync(string search);
    }
}
