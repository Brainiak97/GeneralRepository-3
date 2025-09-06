using FoodService.DAL.Entities;
using FoodService.DAL.Enums;

namespace FoodService.BLL.Interfaces
{
	public interface IFoodService
	{
		/// <summary>
		/// Возвращает продукт
		/// </summary>
		/// <param name="productId">Id искомого продукта</param>
		/// <returns>Продукт, найденный в справочнике</returns>
		Task<Product?> GetProduct( int productId );

		/// <summary>
		/// Возвращает продукты
		/// </summary>
		/// <param name="productName">Имя искомого продукта</param>
		/// <returns>Список продуктов с указанным именем</returns>
		Task<List<Product>> GetProducts( string productName );

		/// <summary>
		/// Добавляет продукт в справочник
		/// </summary>
		/// <param name="productDto">Добавляемый продукт</param>
		/// <returns>Добавленный продукт</returns>
		Task<Product> AddProduct( InfoSourceType infoSourceType, string name, float calories, float? proteins = null, float? fats = null, float? carbs = null );

		/// <summary>
		/// Обновляет поля продукта в справочнике
		/// </summary>
		/// <param name="product">Обновляемый продукт</param>
		/// <returns></returns>
		Task UpdateProduct( Product product );
	}
}
