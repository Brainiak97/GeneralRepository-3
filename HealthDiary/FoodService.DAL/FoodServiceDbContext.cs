using Team3.HealthDiary.FoodService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Team3.HealthDiary.FoodService.DAL
{
	public class FoodServiceDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Meal> Meals { get; set; }
		public DbSet<MealItem> MealItems { get; set; }
		public DbSet<Diet> Diets { get; set; }

		public FoodServiceDbContext( DbContextOptions options )
			: base( options )
		{
		}

		protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
		{
			base.OnConfiguring( optionsBuilder );

			var initProducts = new List<Product>()
			{
				// Овощи и зелень
				new Product("Картофель", 83, 2, 0.1f, 19.7f, 1),
				new Product("Морковь", 33, 1.3f, 0.1f, 7, 1),
				new Product("Свекла", 48, 1.7f, 0f, 10.8f, 1),
				new Product("Огурцы", 15, 0.8f, 0f, 3, 1),
				new Product("Помидоры", 19, 0.6f, 0f, 4.2f, 1),
				new Product("Капуста белокочанная", 28, 1.8f, 0f, 5.4f, 1),
				new Product("Тыква", 29, 1, 0f, 6.5f, 1),
				new Product("Зеленый горошек", 72, 5, 0.2f, 13.3f, 1),

				// Фрукты
				new Product("Яблоки", 46, 0.4f, 0f, 11.3f, 1),
				new Product("Груши", 42, 0.4f, 0f, 10.7f, 1),
				new Product("Апельсины", 38, 0.9f, 0f, 8.4f, 1),
				new Product("Бананы", 89, 1.5f, 0.2f, 21, 1),
				new Product("Виноград", 69, 0.4f, 0f, 17.5f, 1),

				// Крупы и злаки
				new Product("Гречневая крупа", 329, 12.6f, 2.6f, 65.7f, 1),
				new Product("Овсяная крупа", 345, 11.9f, 5.9f, 57.6f, 1),
				new Product("Рисовая крупа", 323, 7, 0.6f, 74.8f, 1),
				new Product("Манная крупа", 326, 11.3f, 0.7f, 71.6f, 1),

				// Бобовые
				new Product("Горох", 72, 5, 0.2f, 13.3f, 1),
				new Product("Фасоль", 307, 22, 1.7f, 49, 1),

				// Молочные продукты
				new Product("Молоко 2.5%", 58, 2.8f, 3.2f, 4.7f, 1),
				new Product("Кефир нежирный", 30, 3, 0.05f, 3.8f, 1),
				new Product("Творог жирный", 226, 14, 18, 1.3f, 1),
				new Product("Сметана 10%", 116, 3, 10, 2.9f, 1),

				// Мясо и птица
				new Product("Говядина", 187, 18.9f, 12.4f, 0, 1),
				new Product("Телятина", 90, 19.7f, 1.2f, 0, 1),
				new Product("Курица", 203, 18.2f, 18.4f, 0.7f, 1),
				new Product("Индейка", 276, 19.5f, 22, 0, 1),

				// Рыба
				new Product("Треска", 75, 17.5f, 0.6f, 0, 1),
				new Product("Лосось", 142, 20, 8.1f, 0, 1),
				new Product("Камбала", 70, 15.7f, 3, 0, 1),

				// Орехи и семена
				new Product("Грецкие орехи", 654, 15.2f, 60.7f, 11.1f, 1),
				new Product("Миндаль", 575, 18.6f, 49.4f, 21.6f, 1),
				new Product("Кешью", 553, 18.2f, 43.9f, 30.2f, 1),

				// Масла
				new Product("Оливковое масло", 898, 0f, 99.8f, 0f, 1),
				new Product("Подсолнечное масло", 899, 0f, 99.9f, 0f, 1),
			};

			// to debug
			//var testMeals = new List<Meal>()
			//{
			//	new Meal( 1, 1, new DateTime( 2025, 01, 15, 09, 0, 0, DateTimeKind.Utc ), "Meal1" ),
			//	new Meal( 2, 1, new DateTime( 2025, 01, 15, 12, 0, 0, DateTimeKind.Utc ), "Meal2" ),
			//};

			//var testMealItems = new List<MealItem>()
			//{
			//	new MealItem( 1, 1, 1 ) { Quantity = 30 },
			//	new MealItem( 2, 1, 2 ) { Quantity = 40 },
			//	new MealItem( 3, 2, 1 ) { Quantity = 50 },
			//	new MealItem( 4, 2, 2 ) { Quantity = 60 },
			//	new MealItem( 5, 2, 3 ) { Quantity = 70 },
			//};

			optionsBuilder.UseSeeding( ( dbContext, _ ) =>
			{
				var anyProduct = dbContext.Set<Product>().FirstOrDefault();
				if ( anyProduct == null )
				{
					dbContext.Set<Product>().AddRange( initProducts );
				}

				// to debug
				//var anyMeal = dbContext.Set<Meal>().FirstOrDefault();
				//if ( anyMeal == null )
				//{
				//	dbContext.Set<Meal>().AddRange( testMeals );
				//}

				//var anyMealItem = dbContext.Set<MealItem>().FirstOrDefault();
				//if ( anyMealItem == null )
				//{
				//	dbContext.Set<MealItem>().AddRange( testMealItems );
				//}

				dbContext.SaveChanges();

				// обновление значения последовательнойсти автоинкремента идентификатора
				//dbContext.Database.ExecuteSqlRaw( "select setval(pg_get_serial_sequence('\"Product\"', 'Id'), (select max(\"Id\") from \"Product\"));" );
			} );
			optionsBuilder.UseAsyncSeeding( async ( dbContext, _, cancellationToken ) =>
			{
				var anyProduct = await dbContext.Set<Product>().FirstOrDefaultAsync();
				if ( anyProduct == null )
				{
					await dbContext.Set<Product>().AddRangeAsync( initProducts );
				}

				// to debug
				//var anyMeal = await dbContext.Set<Meal>().FirstOrDefaultAsync();
				//if ( anyMeal == null )
				//{
				//	await dbContext.Set<Meal>().AddRangeAsync( testMeals );
				//}

				//var anyMealItem = await dbContext.Set<MealItem>().FirstOrDefaultAsync();
				//if ( anyMealItem == null )
				//{
				//	await dbContext.Set<MealItem>().AddRangeAsync( testMealItems );
				//}

				await dbContext.SaveChangesAsync( cancellationToken );

				// обновление значения последовательнойсти автоинкремента идентификатора
				//await dbContext.Database.ExecuteSqlRawAsync( "select setval(pg_get_serial_sequence('\"Product\"', 'Id'), (select max(\"Id\") from \"Product\"));", cancellationToken );
			} );
			optionsBuilder.UseLazyLoadingProxies();
		}

		protected override void OnModelCreating( ModelBuilder modelBuilder )
		{
			base.OnModelCreating( modelBuilder );

			modelBuilder.Entity<Product>().ToTable( "Product" );
			modelBuilder.Entity<Meal>().ToTable( "Meal" );
			modelBuilder.Entity<MealItem>().ToTable( "MealItem" )
				.HasOne( x => x.Meal )
				.WithMany( x => x.MealItems )
				.HasForeignKey( x => x.MealId );
			modelBuilder.Entity<MealItem>()
				.HasOne( x => x.Product )
				.WithMany()
				.HasForeignKey( x => x.ProductId );
			modelBuilder.Entity<Diet>().ToTable( "Diet" );
		}
	}
}
