using System.ComponentModel.DataAnnotations;

namespace FoodService.Api.Contracts.Dtos.Requests
{
	public record AddProductRequest(
		string Name,
		[Required] float Calories,
		float? Proteins = null,
		float? Fats = null,
		float? Carbs = null );
}
