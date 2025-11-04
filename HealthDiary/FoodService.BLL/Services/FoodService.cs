using System.Collections.Concurrent;
using FoodService.BLL.Contracts.Commands;
using FoodService.BLL.Interfaces;
using FoodService.DAL.Entities;
using FoodService.DAL.Repository;

namespace FoodService.BLL.Services
{
	public class FoodService : IFoodService
	{
		private readonly IFoodRepository _foodRepository;
		private ConcurrentDictionary<int, SemaphoreSlim> _userSemaphores;

		public FoodService( IFoodRepository foodRepository, ConcurrentDictionary<int, SemaphoreSlim> userSemaphores )
		{
			_foodRepository = foodRepository;
			_userSemaphores = userSemaphores;
		}

		public async Task<Product?> GetProduct( int productId )
		{
			var product = await _foodRepository.GetByIdAsync<Product, int>( productId );
			return product;
		}

		public async Task<List<Product>> GetProducts( string productName )
		{
			var products = await _foodRepository.GetAll<Product>( x => x.Name == productName );
			return products;
		}

		public async Task<Product> AddProduct( AddProductCommand command )
		{
			var productNew = new Product(
				command.Name,
				command.Calories,
				command.Proteins,
				command.Fats,
				command.Carbs,
				command.InfoSourceType );
			var product = await _foodRepository.AddAsync( productNew );

			return product;
		}

		public async Task UpdateProduct( Product inputProduct )
		{
			await _foodRepository.UpdateAsync<Product, int>( inputProduct );
		}

		public async Task<Meal> AddMeal( int userId, string? mealName )
		{
			var meal = new Meal( userId, mealName );
			meal = await _foodRepository.AddAsync( meal );
			return meal;
		}

		public async Task<Meal?> GetMeal( int mealId )
		{
			var meal = await _foodRepository.GetByIdAsync<Meal, int>( mealId );
			return meal;
		}

		public async Task<MealItem> AddMealItem( AddMealItemCommand command )
		{
			var mealItemNew = new MealItem(
				command.MealId,
				command.ProductId,
				command.Quantity );
			var mealItem = await _foodRepository.AddAsync( mealItemNew );
			return mealItem;
		}

		public async Task<Diet> AddDiet( AddDietCommand command )
		{
			// получаем/добавляем semaphore для текущего пользоватедя
			var semaphore = _userSemaphores.GetOrAdd( command.UserId, new SemaphoreSlim( 1, 1 ) );

			await semaphore.WaitAsync();
			try
			{
				var diets = await _foodRepository.GetAll<Diet>( x => x.UserId == command.UserId );
				if ( diets.Count > 0 )
				{
					throw new InvalidOperationException( $"Для пользователя {command.UserId} уже установлен план питания {diets.First()}" );
				}

				var dietNew = new Diet(
					command.UserId,
					command.Name,
					command.Calories,
					command.Proteins,
					command.Fats,
					command.Carbs );
				var diet = await _foodRepository.AddAsync( dietNew );
				return diet;
			}
			finally
			{
				// освобождаем блокировку
				semaphore.Release();

				// можно было бы удалять семафоры чтобы они не копились (возможна утечка памяти),
				// но это может привести к ситуации, когда два потока войдут в критическую секцию одновременно
				// поэтому храним все симафоры - для небольшого количества пользователей (тысяча) это приемлемо
			}
		}

		public async Task UpdateDiet( Diet inputDiet )
		{
			await _foodRepository.UpdateAsync<Diet, int>( inputDiet );
		}
	}
}
