#pragma warning disable CS8618

using Shared.Common.Interfaces;

namespace FoodService.DAL.Entities
{
	/// <summary>
	/// Элемент приёма пищи
	/// </summary>
	/// <param name="Id">Id</param>
	/// <param name="MealId">MealId</param>
	/// <param name="ProductId">ProductId</param>
	public record MealItem : IEntityModel<int>
	{
		public int Id { get; set; }
		public int MealId { get; set; }
		public int ProductId { get; set; }

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

		public MealItem( int mealId, int productId, float quantity )
		{
			MealId = mealId;
			ProductId = productId;
			Quantity = quantity;
		}
	}
}
