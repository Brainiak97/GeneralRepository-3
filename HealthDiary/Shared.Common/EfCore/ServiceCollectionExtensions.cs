using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Common.EFCore;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Применяет миграции к БД
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceProvider ApplyDbMigration<TDbContext>(this IServiceProvider services)
        where TDbContext : DbContext
    {
        using var scope = services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        dbContext.Database.Migrate();

        return services;
    }
}