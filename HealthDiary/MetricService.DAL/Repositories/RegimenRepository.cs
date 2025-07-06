using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о схеме приема медикаментов пользователем
    /// </summary>
    /// <seealso cref="BaseRepository&lt;Regimen&gt;" />
    /// <seealso cref="IRegimenRepository" />
    public class RegimenRepository : BaseRepository<Regimen>, IRegimenRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="RegimenRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public RegimenRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
        public async override Task<bool> UpdateAsync(Regimen item)
        {
            Regimen? regimen = await GetByIdAsync(item.Id);
            if (regimen != null)
            {
                regimen.Dosage = item.Dosage;
                regimen.Shedule = item.Shedule;
                regimen.StartDate = item.StartDate;
                regimen.EndDate = item.EndDate;
                regimen.Comment = item.Comment;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public async override Task<Regimen?> GetByIdAsync(int id)
        {
            return await _contextDb.Regimens
                .Include(r => r.Medication)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<Regimen>> GetAllAsync()
        {
            return await _contextDb.Regimens
                .Include(r => r.Medication)
                .Include(r => r.User)
                .ToListAsync();
        }
    }
}
