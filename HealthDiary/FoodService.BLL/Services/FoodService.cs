using FoodService.BLL.Interfaces;
using FoodService.DAL.Entities;
using FoodService.DAL.Enums;
using FoodService.DAL.Repository;

namespace FoodService.BLL.Services
{
	public class FoodService : IFoodService
	{
		private readonly IFoodRepository _foodRepository;

		public FoodService( IFoodRepository foodRepository )
		{
			_foodRepository = foodRepository;
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

		public async Task<Product> AddProduct(
			InfoSourceType infoSourceType,
			string name,
			float calories,
			float? proteins = null,
			float? fats = null,
			float? carbs = null )
		{
			var productNew = new Product( name, calories, proteins, fats, carbs, infoSourceType );
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

		public async Task<MealItem> AddMealItem( int mealId, int productId, float quantity )
		{
			var mealItemNew = new MealItem( mealId, productId, quantity );
			var mealItem = await _foodRepository.AddAsync( mealItemNew );
			return mealItem;
		}
	}
}
