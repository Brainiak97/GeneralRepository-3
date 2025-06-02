using EmailService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.DAL.EF
{
    /// <summary>
    /// Контекст базы данных для email-сервиса. 
    /// Предоставляет доступ к сущностям <see cref="EmailTemplate"/> и <see cref="EmailLog"/>.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр контекста базы данных.
    /// </remarks>
    /// <param name="options">Настройки контекста базы данных.</param>
    public class EmailDbContext(DbContextOptions<EmailDbContext> options) : DbContext(options)
    {

        /// <summary>
        /// Возвращает или устанавливает набор сущностей <see cref="EmailTemplate"/> в базе данных.
        /// </summary>
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        /// <summary>
        /// Возвращает или устанавливает набор сущностей <see cref="EmailLog"/> в базе данных.
        /// </summary>
        public DbSet<EmailLog> EmailLogs { get; set; }

        /// <summary>
        /// Настраивает модель базы данных.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели, используемый для настройки схемы.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailTemplate>().HasKey(x => x.Id);
            modelBuilder.Entity<EmailLog>().HasKey(x => x.Id);
        }
    }
}