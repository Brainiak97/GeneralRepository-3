using AutoMapper;
using Team3.HealthDiary.FoodService.DAL.Dtos;
using Team3.HealthDiary.FoodService.DAL.Entities;

namespace Team3.HealthDiary.FoodService.DAL
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ProductDto, Product>().ReverseMap();
		}
	}
}
