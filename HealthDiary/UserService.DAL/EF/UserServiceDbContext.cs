using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.DAL.EF
{
    /// <summary>
    /// Представляет контекст базы данных для пользовательского сервиса.
    /// Содержит DbSet для работы с сущностями <see cref="User"/> и <see cref="Role"/>.
    /// Настраивает связь многие-ко-многим между пользователями и ролями.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр <see cref="UserServiceDbContext"/> с указанными параметрами.
    /// </remarks>
    /// <param name="options">Параметры конфигурации контекста базы данных.</param>
    public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : DbContext(options)
    {

        /// <summary>
        /// Получает или задаёт набор сущностей <see cref="User"/> в базе данных.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Получает или задаёт набор сущностей <see cref="Role"/> в базе данных.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Настраивает модель базы данных. 
        /// Определяет связь многие-ко-многим между пользователями и ролями через таблицу UserRole.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели, используемый для настройки схемы.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(p => p.DateOfBirth)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    j => j
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId"),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                );
        }
    }
}