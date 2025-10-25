using System.ComponentModel.DataAnnotations;

namespace FoodService.Api.Contracts.Dtos.Requests
{
	public record AddDietRequest(
		int UserId,
		string? Name,
		[Required] float Calories,
		[Required] float Proteins,
		[Required] float Fats,
		[Required] float Carbs );
}