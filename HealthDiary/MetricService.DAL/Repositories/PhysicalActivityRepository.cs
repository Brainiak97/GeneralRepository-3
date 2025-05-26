using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    public class PhysicalActivityRepository : ReadBaseRepository<PhysicalActivity>, IPhysicalActivityRepository
    {
        public PhysicalActivityRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }
    }
}
