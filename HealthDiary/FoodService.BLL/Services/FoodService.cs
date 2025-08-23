using Microsoft.EntityFrameworkCore;
using Team3.HealthDiary.FoodService.BLL.Interfaces;
using Team3.HealthDiary.FoodService.DAL;
using Team3.HealthDiary.FoodService.DAL.Entities;
using Team3.HealthDiary.Shared.Common.Exceptions;

namespace Team3.HealthDiary.FoodService.BLL.Services
{
	public class FoodService : IFoodService
	{
		private readonly FoodServiceDbContext _dbContext;

		public FoodService( FoodServiceDbContext dbContext )
		{
			_dbContext = dbContext;
		}

		public async Task<Product?> GetProduct( int productId )
		{
			var product = await _dbContext.Products.SingleOrDefaultAsync( x => x.Id == productId );
			return product;
		}

		public async Task<List<Product>> GetProducts( string productName )
		{
			var products = await _dbContext.Products
				.Where( x => x.Name == productName )
				.ToListAsync();
			return products;
		}

		public async Task<Product> AddProduct(
			byte infoSourceType,
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
			var entry = await _dbContext.AddAsync( productNew );
			var product = entry.Entity;
			await _dbContext.SaveChangesAsync();

			return product;
		}

		public async Task UpdateProduct( Product inputProduct )
		{
			var product = await GetProduct( inputProduct.Id );
			if ( product is null )
			{
				throw new EntryNotFoundException( $"Продукт {inputProduct} не найден" );
			}

			product.Name = inputProduct.Name;
			product.Calories = inputProduct.Calories;
			product.Proteins = inputProduct.Proteins;
			product.Fats = inputProduct.Fats;
			product.Carbs = inputProduct.Carbs;
			product.InfoSourceType = inputProduct.InfoSourceType;

			_dbContext.Update( product );
			await _dbContext.SaveChangesAsync();
		}
	}
}
