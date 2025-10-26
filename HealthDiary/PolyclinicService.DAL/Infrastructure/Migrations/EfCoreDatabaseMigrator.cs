using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PolyclinicService.DAL.Contexts;
using Shared.Common.Migrations.Migrators;

namespace PolyclinicService.DAL.Infrastructure.Migrations;

internal class EfCoreDatabaseMigrator(
    IServiceProvider services,
    ILogger<EfCoreDatabaseMigrator> logger)
    : IDatabaseMigrator
{
    public async Task MigrateAsync()
    {
        try
        {
            var context = services.GetRequiredService<PolyclinicServiceDbContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при инициализации БД");
        }
    }
}