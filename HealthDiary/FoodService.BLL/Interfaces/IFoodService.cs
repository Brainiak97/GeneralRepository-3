using FoodService.BLL.Contracts.Commands;
using FoodService.DAL.Entities;

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
		/// <param name="command">Команда добавления продукта</param>
		/// <returns>Добавленный продукт</returns>
		Task<Product> AddProduct( AddProductCommand command );

		/// <summary>
		/// Обновляет поля продукта в справочнике
		/// </summary>
		/// <param name="product">Обновляемый продукт</param>
		/// <returns></returns>
		Task UpdateProduct( Product product );

		/// <summary>
		/// Добавляет приём пищи
		/// </summary>
		/// <param name="userId">Id пользователя</param>
		/// <param name="mealName">Название приёма пищи</param>
		/// <returns></returns>
		Task<Meal> AddMeal( int userId, string? mealName );

		/// <summary>
		/// Возвращает запись о приёме пищи
		/// </summary>
		/// <param name="mealId">Id приёма пищи</param>
		/// <returns></returns>
		Task<Meal?> GetMeal( int mealId );

		/// <summary>
		/// Добавляет элемент приёма пищи
		/// </summary> 
		/// <param name="command">Команда добавления элемента приёма пищи</param>
		/// <returns></returns>
		Task<MealItem> AddMealItem( AddMealItemCommand command );

		/// <summary>
		/// Добавляет суточный план питания пользователя
		/// </summary>
		/// <param name="command">Команда добавления плана питания</param>
		/// <returns></returns>
		Task<Diet> AddDiet( AddDietCommand command );

		/// <summary>
		/// Обновляет поля плана питания
		/// </summary>
		/// <param name="diet">Обновляемый план питания</param>
		/// <returns></returns>
		Task UpdateDiet( Diet diet );
	}
}
