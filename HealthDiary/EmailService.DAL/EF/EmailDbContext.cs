using EmailService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.DAL.EF
{
    public class EmailDbContext : DbContext
    {
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }

        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailTemplate>().HasKey(x => x.Id);
            modelBuilder.Entity<EmailLog>().HasKey(x => x.Id);
        }
    }
}
