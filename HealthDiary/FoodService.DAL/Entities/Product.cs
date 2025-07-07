namespace FoodService.DAL.Entities
{
	public record Product(
		int Id,
		string Name,
		float Calories,
		float? Proteins,
		float? Fats,
		float? Carbs,
		byte InfoSourceType );
}
