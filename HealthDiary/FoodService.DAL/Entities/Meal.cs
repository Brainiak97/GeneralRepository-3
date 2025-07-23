#pragma warning disable CS8618

namespace Team3.HealthDiary.FoodService.DAL.Entities
{
	/// <summary>
	/// Приём пищи
	/// </summary>
	/// <param name="Id">Id</param>
	/// <param name="UserId">UserId</param>
	/// <param name="Date">Дата</param>
	/// <param name="Name">Название</param>
	public record Meal(
		int Id,
		int UserId,
		DateTime Date,
		string? Name )
	{
		public virtual ICollection<MealItem> MealItems { get; set; }
	}
}
