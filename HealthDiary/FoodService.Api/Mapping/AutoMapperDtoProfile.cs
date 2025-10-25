using AutoMapper;
using FoodService.Api.Contracts.Dtos.Requests;
using FoodService.Api.Contracts.Dtos.Responses;
using FoodService.BLL.Contracts.Commands;
using FoodService.DAL.Entities;
using FoodService.DAL.Enums;
using Dto = FoodService.Api.Contracts.Dtos.Enums;

namespace FoodService.Api.Mapping
{
	public class AutoMapperDtoProfile : Profile
	{
		public AutoMapperDtoProfile()
		{
			CreateMap<AddProductRequest, AddProductCommand>()
				.ConstructUsing( ( source, contex ) =>
				{
					var sourceType = (InfoSourceType)contex.Items[nameof( AddProductCommand.InfoSourceType )];
					return new AddProductCommand(
						InfoSourceType: sourceType,
						Name: source.Name,
						Calories: source.Calories,
						Proteins: source.Proteins,
						Fats: source.Fats,
						Carbs: source.Carbs
					);
				} );
			CreateMap<Dto.InfoSourceType, InfoSourceType>()
				.ReverseMap();
			CreateMap<ProductDto, Product>()
				.ReverseMap();
			CreateMap<Meal, MealDto>()
				.ReverseMap();
			CreateMap<AddMealItemRequest, AddMealItemCommand>();
			CreateMap<MealItem, MealItemDto>()
				.ReverseMap();
			CreateMap<AddDietRequest, AddDietCommand>();
			CreateMap<Diet, DietDto>()
				.ReverseMap();
		}
	}
}
