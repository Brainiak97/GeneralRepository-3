using FoodService.DAL.Enums;

namespace FoodService.BLL.Contracts.Commands
{
	/// <summary>
	/// Команда добавления продукта
	/// </summary>
	/// <param name="InfoSourceType">Тип источника информации</param>
	/// <param name="Name">Название</param>
	/// <param name="Calories">Калорий на 100г</param>
	/// <param name="Proteins">Белков на 100г</param>
	/// <param name="Fats">Жиров на 100г</param>
	/// <param name="Carbs">Углеводов на 100г</param>
	public record AddProductCommand(
		InfoSourceType InfoSourceType,
		string Name,
		float Calories,
		float? Proteins = null,
		float? Fats = null,
		float? Carbs = null );
}
