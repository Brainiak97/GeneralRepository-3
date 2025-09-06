#pragma warning disable CS8618

namespace FoodService.DAL.Entities
{
	/// <summary>
	/// Элемент приёма пищи
	/// </summary>
	/// <param name="Id">Id</param>
	/// <param name="MealId">MealId</param>
	/// <param name="ProductId">ProductId</param>
	public record MealItem(
		int Id,
		int MealId,
		int ProductId )
	{
		/// <summary>
		/// Количество потреблённого продукта, г
		/// </summary>
		public float Quantity { get; set; }

		/// <summary>
		/// Приём пищи
		/// </summary>
		public virtual Meal Meal { get; set; }

		/// <summary>
		/// Потребляемый продукт
		/// </summary>
		public virtual Product Product { get; set; }
	}
}
