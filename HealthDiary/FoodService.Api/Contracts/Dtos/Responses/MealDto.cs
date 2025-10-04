namespace FoodService.Api.Contracts.Dtos.Responses
{
	public record MealDto(
		int Id,
		int UserId,
		DateTime Date,
		string? Name );
}
