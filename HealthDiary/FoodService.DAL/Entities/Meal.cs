#pragma warning disable CS8618

using Shared.Common.Interfaces;

namespace FoodService.DAL.Entities
{
	/// <summary>
	/// Приём пищи
	/// </summary>
	/// <param name="Id">Id</param>
	/// <param name="UserId">UserId</param>
	/// <param name="Date">Дата</param>
	/// <param name="Name">Название</param>
	public record Meal : IEntityModel<int>
	{
		public int Id { get; set; }
		public int UserId { get; init; }
		public DateTime Date { get; set; }
		public string? Name { get; set; }
		public virtual ICollection<MealItem> MealItems { get; set; }

		public Meal( int userId, string? name )
		{
			UserId = userId;
			Date = DateTime.UtcNow;
			Name = name;
		}
	}
}
