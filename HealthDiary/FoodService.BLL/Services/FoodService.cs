using Team3.HealthDiary.FoodService.BLL.Interfaces;
using Team3.HealthDiary.FoodService.DAL.Entities;
using Team3.HealthDiary.FoodService.DAL.Repository;

namespace Team3.HealthDiary.FoodService.BLL.Services
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
			InfoSourceTypeEf infoSourceType,
			string name,
			float calories,
			float? proteins = null,
			float? fats = null,
			float? carbs = null )
		{
			var productNew = new Product( name,
				 calories, proteins, fats, carbs, infoSourceType )
			{
				InfoSourceType = infoSourceType,
				Name = name,
				Calories = calories,
				Proteins = proteins,
				Fats = fats,
				Carbs = carbs,
			};
			var product = await _foodRepository.AddAsync( productNew );

			return product;
		}

		public async Task UpdateProduct( Product inputProduct )
		{
			await _foodRepository.UpdateAsync<Product, int>( inputProduct );
		}
	}
}
