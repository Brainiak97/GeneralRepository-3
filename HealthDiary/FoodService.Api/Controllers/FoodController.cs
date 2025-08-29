using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Team3.HealthDiary.FoodService.BLL.Interfaces;
using Team3.HealthDiary.FoodService.DAL.Dtos;
using Team3.HealthDiary.FoodService.DAL.Entities;
using Team3.HealthDiary.FoodService.DAL.Enums;

namespace Team3.HealthDiary.FoodService.Api.Controllers
{
	[ApiController]
	[Route( "[controller]" )]
	public class FoodController : ControllerBase
	{
		private readonly IMapper _modelMapper;
		private readonly IFoodService _foodService;

		public FoodController( IMapper modelMapper, IFoodService foodService )
		{
			_modelMapper = modelMapper;
			_foodService = foodService;
		}

		[HttpGet( nameof( GetProduct ) )]
		public async Task<IActionResult> GetProduct( int productId )
		{
			var product = await _foodService.GetProduct( productId );

			if ( product != null )
			{
				var productDto = _modelMapper.Map<Product, ProductDto>( product );
				return Ok( productDto );
			}
			else
			{
				return NotFound();
			}
		}

		[HttpGet( nameof( GetProducts ) )]
		public async Task<IActionResult> GetProducts( string productName )
		{
			var products = await _foodService.GetProducts( productName );
			if ( products.Any() )
			{
				var productDtos = _modelMapper.Map<List<ProductDto>>( products );
				return Ok( productDtos );
			}
			else
			{
				return NotFound();
			}
		}

		[HttpPost( nameof( AddProduct ) )]
		public async Task<IActionResult> AddProduct(
			string name,
			[Required] float calories,
			float? proteins = null,
			float? fats = null,
			float? carbs = null )
		{
			var product = await _foodService.AddProduct(
				InfoSourceType.FromUser,
				name,
				calories,
				proteins,
				fats,
				carbs );

			var productDto = _modelMapper.Map<ProductDto>( product );
			return Ok( productDto );
		}

		[HttpPost( nameof( UpdateProduct ) )]
		public async Task<IActionResult> UpdateProduct( int productId, ProductDto productDto )
		{
			var product = _modelMapper.Map<Product>( productDto );
			product.Id = productId;
			await _foodService.UpdateProduct( product );
			return Ok();
		}
	}
}
