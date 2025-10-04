using Shared.Common.Interfaces;

namespace FoodService.DAL.Entities
{
	/// <summary>
	/// Суточный план питания
	/// </summary>
	public record Diet : IEntityModel<int>
	{
		public int Id { get; set; }
		public int UserId { get; init; }
		public string? Name { get; init; }
		public DateTime CreateDate { get; init; }
		public float Calories { get; init; }
		public float Proteins { get; init; }
		public float Fats { get; init; }
		public float Carbs { get; init; }

		/// <summary>
		/// Суточный план питания
		/// </summary>
		/// <param name="userId">UserId</param>
		/// <param name="name">Навзвание плана питения</param>
		/// <param name="calories">Суточная норма потребляемых калорий</param>
		/// <param name="proteins">Суточная норма потребляемых белков</param>
		/// <param name="fats">Суточная норма потребляемых жиров</param>
		/// <param name="carbs">Суточная норма потребляемых углеводов</param>
		public Diet( int userId, string? name, float calories, float proteins, float fats, float carbs )
		{
			UserId = userId;
			Name = name;
			CreateDate = DateTime.UtcNow;
			Calories = calories;
			Proteins = proteins;
			Fats = fats;
			Carbs = carbs;
		}
	}
}
