using AutoMapper;
using Team3.HealthDiary.FoodService.DAL.Dtos;
using Team3.HealthDiary.FoodService.DAL.Entities;

namespace Team3.HealthDiary.FoodService.DAL
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<InfoSourceTypeDto, InfoSourceTypeEf>();
			CreateMap<Product, Product>()
				.ForMember( d => d.Id, opt => opt.Ignore() );
			CreateMap<ProductDto, Product>()
				.ConstructUsing( x => new Product( x.Name, x.Calories, x.Proteins, x.Fats, x.Carbs, (InfoSourceTypeEf)(byte)x.InfoSourceType ) )
				.ReverseMap();
		}
	}
}
