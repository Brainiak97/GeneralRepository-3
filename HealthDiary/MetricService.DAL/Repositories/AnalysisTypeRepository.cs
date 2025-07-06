using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о типе анализа
    /// </summary>
    /// <seealso cref="BaseRepository&lt;AnalysisType&gt;" />
    /// <seealso cref="IAnalysisTypeRepository" />
    public class AnalysisTypeRepository : BaseRepository<AnalysisType>, IAnalysisTypeRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="AnalysisTypeRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public AnalysisTypeRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
        public async override Task<bool> UpdateAsync(AnalysisType item)
        {
            AnalysisType? analysisType = await GetByIdAsync(item.Id);
            if (analysisType != null)
            {
                analysisType.Unit = item.Unit;
                analysisType.Name = item.Name;
                analysisType.ReferenceValueMale = item.ReferenceValueMale;
                analysisType.ReferenceValueFemale = item.ReferenceValueFemale;
                analysisType.AnalysisCategoryId = item.AnalysisCategoryId;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public async override Task<AnalysisType?> GetByIdAsync(int id)
        {
            return await _contextDb.AnalysisTypes
                .Include(a => a.AnalysisCategory)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<AnalysisType>> GetAllAsync()
        {
            return await _contextDb.AnalysisTypes
                .Include(a => a.AnalysisCategory)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AnalysisType>> GetListAnalysisTypeBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');
            var workRecords = await _contextDb.AnalysisTypes.ToListAsync();
            var filterRecords = new List<AnalysisType>();
           
            foreach (var item in stringsSearch)
            {               
                filterRecords.AddRange(workRecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase)));
            }

            return filterRecords;
        }
    }
}
