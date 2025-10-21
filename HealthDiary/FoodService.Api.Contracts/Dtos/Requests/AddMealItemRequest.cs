namespace FoodService.Api.Contracts.Dtos.Requests
{
	public record AddMealItemRequest(
		int MealId,
		int ProductId,
		float Quantity );
}