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
		/// <param name="mealId">Приём пищи, для которого добавляется элемент</param>
		/// <param name="productId">Потребляемый продукт</param>
		/// <param name="quantity">Количество продукта, г</param>
		/// <returns></returns>
		Task<MealItem> AddMealItem( int mealId, int productId, float quantity );

		/// <summary>
		/// Добавляет суточный план питания пользователя
		/// </summary>
		/// <param name="userId">Id пользователя, для которого предназначен план питания</param>
		/// <param name="name">Навзвание плана питения</param>
		/// <param name="calories">Суточная норма потребляемых калорий</param>
		/// <param name="proteins">Суточная норма потребляемых белков</param>
		/// <param name="fats">Суточная норма потребляемых жиров</param>
		/// <param name="carbs">Суточная норма потребляемых углеводов</param>
		/// <returns></returns>
		Task<Diet> AddDiet( int userId, string? name, float calories, float proteins, float fats, float carbs );

		/// <summary>
		/// Обновляет поля плана питания
		/// </summary>
		/// <param name="diet">Обновляемый план питания</param>
		/// <returns></returns>
		Task UpdateDiet( Diet diet );
	}
}
