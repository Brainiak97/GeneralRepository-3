using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Team3.HealthDiary.FoodService.DAL
{
	public static class EntityFrameworkExtensions
	{
		/// <summary>
		/// Добавляет EF контекст для БД Postgres
		/// </summary>
		/// <param name="services"></param>
		/// <param name="connectionString">Строка подключения к БД Postgres</param>
		/// <returns></returns>
		public static IServiceCollection AddPgFoodServiceDbContext( this IServiceCollection services, string connectionString )
		{
			services.AddDbContext<FoodServiceDbContext>( options => options.UseNpgsql( connectionString ) );
			return services;
		}

		/// <summary>
		/// Применяет миграции к БД
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceProvider ApplyDbMigration( this IServiceProvider services )
		{
			using var scope = services.CreateScope();
			using var dbContext = scope.ServiceProvider.GetRequiredService<FoodServiceDbContext>();
			dbContext.Database.Migrate();

			return services;
		}
	}
}
