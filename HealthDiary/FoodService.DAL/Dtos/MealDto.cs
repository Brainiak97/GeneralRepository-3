namespace FoodService.DAL.Dtos
{
	public record MealDto(
		int Id,
		int UserId,
		DateTime Date,
		string? Name );
}
