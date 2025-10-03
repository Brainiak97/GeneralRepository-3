using AutoMapper;
using FoodService.Api.Contracts.Dtos.Requests;
using FoodService.Api.Contracts.Dtos.Responses;
using FoodService.BLL.Contracts.Commands;
using FoodService.BLL.Interfaces;
using FoodService.DAL.Entities;
using FoodService.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Api.Controllers
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
		public async Task<IActionResult> AddProduct( AddProductRequest request )
		{
			var command = _modelMapper.Map<AddProductCommand>( request, opt =>
			{
				opt.Items[nameof( AddProductCommand.InfoSourceType )] = InfoSourceType.FromUser;
			} );
			var product = await _foodService.AddProduct( command );
			var productDto = _modelMapper.Map<ProductDto>( product );

			return Ok( productDto );
		}

		[HttpPost( nameof( UpdateProduct ) )]
		public async Task<IActionResult> UpdateProduct( ProductDto productDto )
		{
			var product = _modelMapper.Map<Product>( productDto );
			await _foodService.UpdateProduct( product );

			return Ok();
		}

		[HttpPost( nameof( AddMeal ) )]
		public async Task<IActionResult> AddMeal( int userId, string? mealName )
		{
			// TODO проверка пользователя через UserService

			var meal = await _foodService.AddMeal( userId, mealName );
			var mealDto = _modelMapper.Map<MealDto>( meal );

			return Ok( mealDto );
		}

		[HttpPost( nameof( AddMealItem ) )]
		public async Task<IActionResult> AddMealItem( AddMealItemRequest request )
		{
			var command = _modelMapper.Map<AddMealItemCommand>( request );
			var mealItem = await _foodService.AddMealItem( command );
			var mealItemDto = _modelMapper.Map<MealItemDto>( mealItem );

			return Ok( mealItemDto );
		}

		[HttpPost( nameof( AddDiet ) )]
		public async Task<IActionResult> AddDiet( AddDietRequest request )
		{
			// TODO проверка пользователя через UserService

			var command = _modelMapper.Map<AddDietCommand>( request );
			var diet = await _foodService.AddDiet( command );
			var dietDto = _modelMapper.Map<DietDto>( diet );

			return Ok( dietDto );
		}

		[HttpPost( nameof( UpdateDiet ) )]
		public async Task<IActionResult> UpdateDiet( DietDto dietDto )
		{
			var diet = _modelMapper.Map<Diet>( dietDto );
			await _foodService.UpdateDiet( diet );

			return Ok();
		}
	}
}
