using AutoMapper;
using FoodService.Api.Contracts.Dtos.Requests;
using FoodService.Api.Contracts.Dtos.Responses;
using FoodService.BLL.Contracts.Commands;
using FoodService.DAL.Entities;
using FoodService.DAL.Enums;

namespace FoodService.Api.Contracts
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
			CreateMap<ProductDto, Product>()
				.ConstructUsing( x => new Product( x.Name, x.Calories, x.Proteins, x.Fats, x.Carbs, x.InfoSourceType ) )
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
