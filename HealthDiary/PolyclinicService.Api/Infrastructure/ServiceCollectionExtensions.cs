using Shared.Common.Migrations.Migrators;

namespace PolyclinicService.Api.Infrastructure;

internal static class ServiceCollectionExtensions
{
    public static async Task InitializeServiceDatabaseAsync(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
        await migrator.MigrateAsync();
    }
}