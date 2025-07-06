using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о результате анализа пользователя
    /// </summary>
    /// <seealso cref="AnalysisResult" />
    public interface IAnalysisResultRepository : IRepository<AnalysisResult>
    {
    }
}
