using FoodService.Api.Contracts.Dtos.Requests;
using FoodService.Api.Contracts.Dtos.Responses;
using Refit;

namespace FoodService.Api.Contracts
{
	// TODO Добавить обработку случаев, когда запрос возвращает 404
	public interface IFoodServiceClient
	{
		[Get( $"/{nameof( GetProduct )}" )]
		Task<ProductDto?> GetProduct( int productId );

		[Get( $"/{nameof( GetProducts )}" )]
		Task<List<ProductDto>> GetProducts( string productName );

		[Post( $"/{nameof( AddProduct )}" )]
		Task<ProductDto> AddProduct( AddProductRequest request );

		[Put( $"/{nameof( UpdateProduct )}" )]
		Task UpdateProduct( ProductDto productDto );

		[Post( $"/{nameof( AddMeal )}" )]
		Task<MealDto> AddMeal( int userId, string? mealName );

		[Post( $"/{nameof( AddMealItem )}" )]
		Task<MealItemDto> AddMealItem( AddMealItemRequest request );

		[Post( $"/{nameof( AddDiet )}" )]
		Task<DietDto> AddDiet( AddDietRequest request );

		[Put( $"/{nameof( UpdateDiet )}" )]
		Task UpdateDiet( DietDto dietDto );
	}
}
