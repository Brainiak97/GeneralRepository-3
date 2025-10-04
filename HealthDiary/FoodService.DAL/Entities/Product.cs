using FoodService.DAL.Enums;
using Shared.Common.Interfaces;

namespace FoodService.DAL.Entities
{
	/// <summary>
	/// Продукт питания
	/// </summary>
	public record Product : IEntityModel<int>
	{
		public int Id { get; set; }
		public InfoSourceType InfoSourceType { get; set; }
		public string Name { get; set; }
		public float Calories { get; set; }
		public float? Proteins { get; set; }
		public float? Fats { get; set; }
		public float? Carbs { get; set; }

		/// <summary>
		/// Продукт питания
		/// </summary>
		/// <param name="infoSourceType">Источник информации о продукте</param>
		/// <param name="name">Название</param>
		/// <param name="calories">Калории на 100г</param>
		/// <param name="proteins">Белки на 100г</param>
		/// <param name="fats">Жиры на 100г</param>
		/// <param name="carbs">Углеводы на 100г</param>
		public Product( string name, float calories, float? proteins, float? fats, float? carbs, InfoSourceType infoSourceType )
		{
			InfoSourceType = infoSourceType;
			Name = name;
			Calories = calories;
			Proteins = proteins;
			Fats = fats;
			Carbs = carbs;
		}
	}
}
