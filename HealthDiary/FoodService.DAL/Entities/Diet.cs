namespace Team3.HealthDiary.FoodService.DAL.Entities
{
	/// <summary>
	/// Суточный план питания
	/// </summary>
	/// <param name="Id">Id</param>
	/// <param name="UserId">UserId</param>
	/// <param name="Name">Название плана питания</param>
	/// <param name="CreateDate">Дата создания</param>
	/// <param name="Calories">Норма калорий в сутки</param>
	/// <param name="Proteins">Норма белков в сутки</param>
	/// <param name="Fats">Норма жиров в сутки</param>
	/// <param name="Carbs">Норма углеводов в сутки</param>
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
