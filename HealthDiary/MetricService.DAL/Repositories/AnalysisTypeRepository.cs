using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class AnalysisTypeRepository : BaseRepository<AnalysisType>, IAnalysisTypeRepository
    {
        public AnalysisTypeRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

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

        public async override Task<AnalysisType?> GetByIdAsync(int id)
        {
            return await _contextDb.AnalysisTypes
                .Include(a => a.AnalysisCategory)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async override Task<IEnumerable<AnalysisType>> GetAllAsync()
        {
            return await _contextDb.AnalysisTypes
                .Include(a => a.AnalysisCategory)
                .ToListAsync();
        }
    }
}
