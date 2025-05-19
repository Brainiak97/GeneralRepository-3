using EmailService.DAL.EF;
using EmailService.DAL.Interfaces;
using EmailService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.DAL.Repositories
{
    public class EmailLogRepository : IRepository<EmailLog>
    {
        private readonly EmailDbContext _context;

        public EmailLogRepository(EmailDbContext context) => _context = context;

        public async Task<EmailLog?> GetByIdAsync(Guid id) =>
            await _context.EmailLogs.FindAsync(id);

        public async Task<IEnumerable<EmailLog>?> GetAllAsync() =>
            await _context.EmailLogs.ToListAsync();

        public async Task<EmailLog?> AddAsync(EmailLog log)
        {
            await _context.EmailLogs.AddAsync(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<EmailLog?> UpdateAsync(EmailLog log)
        {
            _context.EmailLogs.Update(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task DeleteAsync(EmailLog log)
        {
            _context.EmailLogs.Remove(log);
            await _context.SaveChangesAsync();
        }
    }
}
