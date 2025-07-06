namespace PolyclinicService.DAL.Infrastructure.Migrations;

/// <summary>
/// Предоставляет методы для миграции базы данных микросервиса.
/// </summary>
public interface IDatabaseMigrator
{
    /// <summary>
    /// Мигрировать базу данных микросервиса.
    /// </summary>
    /// <returns><see cref="Task"/>.</returns>
    Task MigrateAsync();
}