using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class IntakeRepository : BaseRepository<Intake>, IIntakeRepository
    {
        public IntakeRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        public async override Task<bool> UpdateAsync(Intake item)
        {
            Intake? intake = await GetByIdAsync(item.Id);
            if (intake != null)
            {
                intake.TakenAt = item.TakenAt;
                intake.IntakeStatus = item.IntakeStatus;
                intake.RegimenId = item.RegimenId;
                intake.Comment = item.Comment;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        public async override Task<Intake?> GetByIdAsync(int id)
        {
            return await _contextDb.Intakes
                .Include(i => i.Regimen)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async override Task<IEnumerable<Intake>> GetAllAsync()
        {
            return await _contextDb.Intakes
                .Include(i => i.Regimen)
                .ToListAsync();
        }
    }
}
