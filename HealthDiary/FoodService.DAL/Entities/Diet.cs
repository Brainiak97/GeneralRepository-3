namespace FoodService.DAL.Entities
{
	/// <summary>
	/// Суточный план питания
	/// </summary>
	/// <param name="Id"></param>
	/// <param name="UserId"></param>
	/// <param name="Name"></param>
	/// <param name="CreateDate"></param>
	/// <param name="Calories"></param>
	/// <param name="Proteins"></param>
	/// <param name="Fats"></param>
	/// <param name="Carbs"></param>
	public record Diet(
		int Id,
		int UserId,
		string? Name,
		DateTime CreateDate,
		float Calories,
		float Proteins,
		float Fats,
		float Carbs );
}
