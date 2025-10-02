using AutoMapper;
using FoodService.DAL.Dtos;
using FoodService.DAL.Entities;

namespace FoodService.DAL
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Product, Product>()
				.ForMember( d => d.Id, opt => opt.Ignore() );
			CreateMap<ProductDto, Product>()
				.ConstructUsing( x => new Product( x.Name, x.Calories, x.Proteins, x.Fats, x.Carbs, x.InfoSourceType ) )
				.ReverseMap();
			CreateMap<Meal, MealDto>()
				.ReverseMap();
			CreateMap<MealItem, MealItemDto>()
				.ReverseMap();
		}
	}
}
