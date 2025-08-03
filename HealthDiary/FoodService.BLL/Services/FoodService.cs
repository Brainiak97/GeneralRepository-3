using Team3.HealthDiary.FoodService.BLL.Interfaces;
using Team3.HealthDiary.FoodService.DAL;
using Team3.HealthDiary.FoodService.DAL.Entities;

namespace Team3.HealthDiary.FoodService.BLL.Services
{
	public class FoodService : IFoodService
	{
		private readonly FoodServiceDbContext _dbContext;

		public FoodService( FoodServiceDbContext dbContext )
		{
			_dbContext = dbContext;
		}

		public Task<Product> GetProduct( int productId )
		{
			throw new NotImplementedException();
		}

		public Task<List<Product>> GetProducts( string productName )
		{
			throw new NotImplementedException();
		}
		public Task<Product> AddProduct( byte infoSourceType, string name, float calories, float? proteins = null, float? fats = null, float? carbs = null )
		{
			throw new NotImplementedException();
		}

		public Task UpdateProduct( Product product )
		{
			throw new NotImplementedException();
		}
	}
}
