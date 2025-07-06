using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о приеме медикаментов пользователем
    /// </summary>
    /// <seealso cref="BaseRepository&lt;Intake&gt;" />
    /// <seealso cref="IIntakeRepository" />
    public class IntakeRepository : BaseRepository<Intake>, IIntakeRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="IntakeRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public IntakeRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async override Task<Intake?> GetByIdAsync(int id)
        {
            return await _contextDb.Intakes
                .Include(i => i.Regimen)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<Intake>> GetAllAsync()
        {
            return await _contextDb.Intakes
                .Include(i => i.Regimen)
                .ToListAsync();
        }
    }
}
