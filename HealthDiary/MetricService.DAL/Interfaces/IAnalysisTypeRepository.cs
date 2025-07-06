using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о типе анализов
    /// </summary>
    /// <seealso cref="AnalysisType" />
    public interface IAnalysisTypeRepository : IRepository<AnalysisType>
    {
        /// <summary>
        /// Получение списка записей из справочника, заданной строкой поиска по наименованию.
        /// В строку поиска можно передать несколько фраз поиска через ","
        /// </summary>
        /// <param name="search">Строка поиска по наименованию</param>
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<AnalysisType>> GetListAnalysisTypeBySearchAsync(string search);
    }
}
