using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    public class AnalysisCategoryRepository : BaseRepository<AnalysisCategory>, IAnalysisCategoryRepository
    {
        public AnalysisCategoryRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }       

        public async  override Task<bool> UpdateAsync(AnalysisCategory item)
        {
            AnalysisCategory? analysisCategory = await GetByIdAsync(item.Id);
            if (analysisCategory != null)
            {
                analysisCategory.Description = item.Description;
                analysisCategory.Name = item.Name;                
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
