using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FoodService.BLL.Interfaces;
using FoodService.DAL.Dtos;
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

		[HttpPost( nameof( AddMeal ) )]
		public async Task<IActionResult> AddMeal( int userId, string? mealName )
		{
			// TODO проверка пользователя через UserService

			var meal = await _foodService.AddMeal( userId, mealName );
			var mealDto = _modelMapper.Map<MealDto>( meal );
			return Ok( mealDto );
		}

		[HttpPost( nameof( AddMealItem ) )]
		public async Task<IActionResult> AddMealItem( int mealId, int productId, float quantity )
		{
			var mealItem = await _foodService.AddMealItem( mealId, productId, quantity );
			var mealItemDto = _modelMapper.Map<MealItemDto>( mealItem );

			return Ok( mealItemDto );
		}

		[HttpPost( nameof( AddDiet ) )]
		public async Task<IActionResult> AddDiet(
			int userId,
			string? name,
			[Required] float calories,
			[Required] float proteins,
			[Required] float fats,
			[Required] float carbs )
		{
			// TODO проверка пользователя через UserService

			var diet = await _foodService.AddDiet(
					userId,
					name,
					calories,
					proteins,
					fats,
					carbs );

			var dietDto = _modelMapper.Map<DietDto>( diet );
			return Ok( dietDto );
		}

		[HttpPost( nameof( UpdateDiet ) )]
		public async Task<IActionResult> UpdateDiet( int dietId, DietDto dietDto )
		{
			var diet = _modelMapper.Map<Diet>( dietDto );
			diet.Id = dietId;
			await _foodService.UpdateDiet( diet );
			return Ok();
		}
	}
}
