using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    public class PhysicalActivityRepository : BaseRepository<PhysicalActivity>, IPhysicalActivityRepository
    {
        public PhysicalActivityRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }
        public async override Task<bool> CreateAsync(PhysicalActivity item)
        {
            item.Id = 0;
            _contextDb.Add(item);
            return await _contextDb.SaveChangesAsync() == 1;
        }

        public async override Task<bool> UpdateAsync(PhysicalActivity item)
        {
            PhysicalActivity? physicalActivity = await GetByIdAsync(item.Id);
            if (physicalActivity != null)
            {
                physicalActivity.EnergyEquivalent = item.EnergyEquivalent;
                physicalActivity.Name = item.Name;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
