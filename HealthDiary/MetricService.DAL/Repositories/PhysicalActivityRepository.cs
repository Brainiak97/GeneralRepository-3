using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о физической активности
    /// </summary>
    /// <seealso cref="BaseRepository&lt;PhysicalActivity&gt;" />
    /// <seealso cref="IPhysicalActivityRepository" />
    public class PhysicalActivityRepository : BaseRepository<PhysicalActivity>, IPhysicalActivityRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="PhysicalActivityRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public PhysicalActivityRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>       
        public async  Task<IEnumerable<PhysicalActivity>> GetListPhysicalActivitiesBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');

            var workRecords = await _contextDb.PhysicalActivities.ToListAsync();
            var filterRecords = new List<PhysicalActivity>();            
            foreach (var item in stringsSearch)
            {                
                filterRecords.AddRange(workRecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase)));
            }

            return filterRecords;
        }
    }
}
