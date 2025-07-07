#pragma warning disable CS8618

namespace FoodService.DAL.Entities
{
	/// <summary>
	/// Приём пищи
	/// </summary>
	/// <param name="Id"></param>
	/// <param name="UserId"></param>
	/// <param name="Date"></param>
	/// <param name="Name"></param>
	public record Meal(
		int Id,
		int UserId,
		DateTime Date,
		string? Name )
	{
		public virtual ICollection<MealItem> MealItems { get; set; }
	}
}
