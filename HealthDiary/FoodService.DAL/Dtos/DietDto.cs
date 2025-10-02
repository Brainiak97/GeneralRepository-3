namespace FoodService.DAL.Dtos
{
	public record DietDto(
		int UserId,
		string Name,
		DateTime CreateDate,
		float Calories,
		float Proteins,
		float Fats,
		float Carbs );
}
