using AutoMapper;
using FoodService.Api.Contracts.Dtos.Responses;
using FoodService.DAL.Entities;

namespace FoodService.Api.Contracts
{
	public class AutoMapperDtoProfile : Profile
	{
		public AutoMapperDtoProfile()
		{
			CreateMap<ProductDto, Product>()
				.ConstructUsing( x => new Product( x.Name, x.Calories, x.Proteins, x.Fats, x.Carbs, x.InfoSourceType ) )
				.ReverseMap();
			CreateMap<Meal, MealDto>()
				.ReverseMap();
			CreateMap<MealItem, MealItemDto>()
				.ReverseMap();
			CreateMap<Diet, DietDto>()
				.ReverseMap();
		}
	}
}
