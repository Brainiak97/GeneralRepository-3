using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о форме выпуска препарата
    /// </summary>
    /// <seealso cref="BaseRepository&lt;DosageForm&gt;" />
    /// <seealso cref="IDosageFormRepository" />
    public class DosageFormRepository : BaseRepository<DosageForm>, IDosageFormRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="DosageFormRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public DosageFormRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
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
