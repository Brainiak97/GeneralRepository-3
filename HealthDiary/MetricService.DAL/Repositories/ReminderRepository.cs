using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
    {
        public ReminderRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

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

        public async override Task<Reminder?> GetByIdAsync(int id)
        {
            return await _contextDb.Reminders
                .Include(r => r.Regimen)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async override Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _contextDb.Reminders
                .Include(r => r.Regimen)
                .ToListAsync();
        }
    }
}
