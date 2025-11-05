using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.DAL.EF;
using UserService.Domain.Models;

namespace UserService.Api.Data
{
    /// <summary>
    /// Предоставляет методы для инициализации базы данных при запуске приложения.
    /// Включает создание ролей и начального пользователя (администратора).
    /// </summary>
    public static class InitializeDatabase
    {
        /// <summary>
        /// Асинхронно инициализирует базу данных: применяет миграции, создаёт роли и начального администратора.
        /// </summary>
        /// <param name="serviceProvider">Поставщик сервисов для получения контекста базы данных.</param>
        /// <param name="passwordHasher">Сервис для хеширования паролей пользователей.</param>
        public static async Task Initialize(IServiceProvider serviceProvider, PasswordHasher<User> passwordHasher)
        {
            var context = serviceProvider.GetRequiredService<UserServiceDbContext>();
            await context.Database.MigrateAsync();

            // Создание ролей
            var roles = new List<Role>
            {
                new() { Name = "User" },
                new() { Name = "Admin" },
                new() { Name = "Doctor" },
            };

            foreach (var role in roles)
            {
                if (!await context.Roles.AnyAsync(r => r.Name == role.Name))
                    await context.Roles.AddAsync(role);
            }

            await context.SaveChangesAsync();

            // Создание администратора
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Username == "admin");

            if (adminUser == null)
            {
                adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "1234567890",
                    PasswordHash = string.Empty
                };
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");
                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }

            // Назначение роли Admin
            if (adminRole != null)
            {
                adminUser.Roles.Add(adminRole);
                await context.SaveChangesAsync();
            }
        }
    }
}