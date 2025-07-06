using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о напоминании примема медикаментов пользователем
    /// </summary>
    /// <seealso cref="BaseRepository&lt;Reminder&gt;" />
    /// <seealso cref="IReminderRepository" />
    public class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория <see cref="ReminderRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public ReminderRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
        public override async Task<bool> UpdateAsync(Reminder item)
        {
            Reminder? reminder = await GetByIdAsync(item.Id);
            if (reminder != null)
            {
                reminder.RemindAt = item.RemindAt;
                reminder.IsSend = item.IsSend;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public async override Task<Reminder?> GetByIdAsync(int id)
        {
            return await _contextDb.Reminders
                .Include(r => r.Regimen)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _contextDb.Reminders
                .Include(r => r.Regimen)
                .ToListAsync();
        }
    }
}
