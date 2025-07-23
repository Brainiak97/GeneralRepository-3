namespace Team3.HealthDiary.FoodService.DAL.Entities
{
	/// <summary>
	/// Продукт питания
	/// </summary>
	/// <param name="Id">Id</param>
	/// <param name="Name">Название</param>
	/// <param name="Calories">Калории на 100г</param>
	/// <param name="Proteins">Белки на 100г</param>
	/// <param name="Fats">Жиры на 100г</param>
	/// <param name="Carbs">Углеводы на 100г</param>
	/// <param name="InfoSourceType">Источник информации о продукте</param>
	public record Product(
		int Id,
		string Name,
		float Calories,
		float? Proteins,
		float? Fats,
		float? Carbs,
		byte InfoSourceType );
}
