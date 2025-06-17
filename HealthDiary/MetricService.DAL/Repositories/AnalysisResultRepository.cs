using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class AnalysisResultRepository : BaseRepository<AnalysisResult>, IAnalysisResultRepository
    {
        public AnalysisResultRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

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

        public async override Task<AnalysisResult?> GetByIdAsync(int id)
        {
            return await _contextDb.AnalysisResults
                .Include(a => a.User)
                .Include(a=>a.AnalysisType)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async override Task<IEnumerable<AnalysisResult>> GetAllAsync()
        {
            return await _contextDb.AnalysisResults
                .Include(a => a.User)
                .Include(a=>a.AnalysisType)
                .ToListAsync();
        }
    }
}
