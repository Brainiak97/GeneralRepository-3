using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    class DosageFormRepository : BaseRepository<DosageForm>, IDosageFormRepository
    {
        public DosageFormRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        public async override Task<bool> UpdateAsync(DosageForm item)
        {
            DosageForm? dosageForm = await GetByIdAsync(item.Id);
            if (dosageForm != null)
            {
                dosageForm.Name = item.Name;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
