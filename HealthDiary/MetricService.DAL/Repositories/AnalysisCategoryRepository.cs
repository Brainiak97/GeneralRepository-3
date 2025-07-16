using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о категории анализов
    /// </summary>
    /// <seealso cref="BaseRepository&lt;AnalysisCategory&gt;" />
    /// <seealso cref="IAnalysisCategoryRepository" />
    public class AnalysisCategoryRepository : BaseRepository<AnalysisCategory>, IAnalysisCategoryRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="AnalysisCategoryRepository"/>.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public AnalysisCategoryRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }


        /// <inheritdoc />
        public async override Task<bool> UpdateAsync(AnalysisCategory item)
        {
            AnalysisCategory? analysisCategory = await GetByIdAsync(item.Id);
            if (analysisCategory != null)
            {
                analysisCategory.Description = item.Description;
                analysisCategory.Name = item.Name;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>        
        public async Task<IEnumerable<AnalysisCategory>> GetListAnalysisCategoriesBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');
            var workRecords = await _contextDb.AnalysisCategories.ToListAsync();
            var filterRecords = new List<AnalysisCategory>();            
            foreach (var item in stringsSearch)
            {              
                filterRecords.AddRange(workRecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase)));
            }

            return filterRecords;
        }
    }
}
