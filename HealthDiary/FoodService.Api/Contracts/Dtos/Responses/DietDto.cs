namespace FoodService.Api.Contracts.Dtos.Responses
{
	public record DietDto(
		int Id,
		int UserId,
		string Name,
		DateTime CreateDate,
		float Calories,
		float Proteins,
		float Fats,
		float Carbs );
}
