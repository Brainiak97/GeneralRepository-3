using FoodService.DAL.Enums;

namespace FoodService.Api.Contracts.Dtos.Responses
{
	/// <summary>
	/// Продукт питания
	/// </summary> 
	/// <param name="InfoSourceType">Источник информации о продукте</param>
	/// <param name="Name">Название</param>
	/// <param name="Calories">Калории на 100г</param>
	/// <param name="Proteins">Белки на 100г</param>
	/// <param name="Fats">Жиры на 100г</param>
	/// <param name="Carbs">Углеводы на 100г</param>
	public record ProductDto(
		int Id,
		InfoSourceType InfoSourceType,
		string Name,
		float Calories,
		float? Proteins,
		float? Fats,
		float? Carbs );
}
