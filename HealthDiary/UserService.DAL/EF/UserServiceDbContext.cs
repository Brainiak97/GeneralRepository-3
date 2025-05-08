using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.DAL.EF
{
    public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настраиваем связь многие-ко-многим через навигационные свойства
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
