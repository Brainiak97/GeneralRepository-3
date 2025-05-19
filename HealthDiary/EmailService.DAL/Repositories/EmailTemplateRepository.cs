using EmailService.DAL.EF;
using EmailService.DAL.Interfaces;
using EmailService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.DAL.Repositories
{
    public class EmailTemplateRepository : IRepository<EmailTemplate>
    {
        private readonly EmailDbContext _context;

        public EmailTemplateRepository(EmailDbContext context) => _context = context;

        public async Task<EmailTemplate?> GetByIdAsync(Guid id) =>
            await _context.EmailTemplates.FindAsync(id);

        public async Task<IEnumerable<EmailTemplate>?> GetAllAsync() =>
            await _context.EmailTemplates.ToListAsync();

        public async Task<EmailTemplate?> AddAsync(EmailTemplate template)
        {
            await _context.AddAsync(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task<EmailTemplate?> UpdateAsync(EmailTemplate template)
        {
            _context.EmailTemplates.Update(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task DeleteAsync(EmailTemplate template)
        {
            _context.EmailTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }
    }
}
