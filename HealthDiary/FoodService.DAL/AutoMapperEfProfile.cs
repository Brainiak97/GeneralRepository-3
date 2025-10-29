using AutoMapper;
using FoodService.DAL.Entities;

namespace FoodService.DAL
{
	public class AutoMapperEfProfile : Profile
	{
		public AutoMapperEfProfile()
		{
			CreateMap<Product, Product>()
				.ForMember( d => d.Id, opt => opt.Ignore() );
			CreateMap<Diet, Diet>()
				.ForMember( x => x.Id, opt => opt.Ignore() )
				.ForMember( x => x.CreateDate, opt => opt.Ignore() );
		}
	}
}
