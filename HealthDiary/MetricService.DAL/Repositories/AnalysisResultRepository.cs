using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о результате анализа пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;AnalysisResult&gt;" />
    /// <seealso cref="IAnalysisResultRepository" />
    public class AnalysisResultRepository : BaseRepository<AnalysisResult>, IAnalysisResultRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="AnalysisResultRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public AnalysisResultRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
        public async override Task<bool> UpdateAsync(AnalysisResult item)
        {
            AnalysisResult? analysisResult = await GetByIdAsync(item.Id);
            if (analysisResult != null)
            {
                analysisResult.UserId = item.UserId;
                analysisResult.AnalysisTypeId= item.AnalysisTypeId;
                analysisResult.Value=item.Value;
                analysisResult.DetailedResearchDescription=item.DetailedResearchDescription;
                analysisResult.TestedAt=item.TestedAt;
                analysisResult.Comment=item.Comment;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public async override Task<AnalysisResult?> GetByIdAsync(int id)
        {
            return await _contextDb.AnalysisResults
                .Include(a => a.User)
                .Include(a=>a.AnalysisType)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<AnalysisResult>> GetAllAsync()
        {
            return await _contextDb.AnalysisResults
                .Include(a => a.User)
                .Include(a=>a.AnalysisType)
                .ToListAsync();
        }
    }
}
