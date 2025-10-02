#pragma warning disable CS8618

namespace FoodService.DAL.Dtos
{
	public record MealItemDto
	{
		public int Id { get; set; }

		/// <summary>
		/// Количество потреблённого продукта, г
		/// </summary>
		public float Quantity { get; set; }

		/// <summary>
		/// Приём пищи
		/// </summary>
		public MealDto Meal { get; set; }

		/// <summary>
		/// Потребляемый продукт
		/// </summary>
		public ProductDto Product { get; set; }
	}
}
