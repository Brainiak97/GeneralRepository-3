namespace FoodService.BLL.Contracts.Commands
{
	/// <summary>
	/// Команда добавления плана питания
	/// </summary>
	/// <param name="UserId">Id пользователя, для которого предназначен план питания</param>
	/// <param name="Name">Навзвание плана питения</param>
	/// <param name="Calories">Суточная норма потребляемых калорий</param>
	/// <param name="Proteins">Суточная норма потребляемых белков</param>
	/// <param name="Fats">Суточная норма потребляемых жиров</param>
	/// <param name="Carbs">Суточная норма потребляемых углеводов</param>
	public record AddDietCommand(
		int UserId,
		string? Name,
		float Calories,
		float Proteins,
		float Fats,
		float Carbs );
}
