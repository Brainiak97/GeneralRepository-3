namespace Team3.HealthDiary.FoodService.DAL.Entities
{
	/// <summary>
	/// Продукт питания
	/// </summary>
	/// <param name="Id">Id</param>
	/// <param name="InfoSourceType">Источник информации о продукте</param>
	/// <param name="Name">Название</param>
	/// <param name="Calories">Калории на 100г</param>
	/// <param name="Proteins">Белки на 100г</param>
	/// <param name="Fats">Жиры на 100г</param>
	/// <param name="Carbs">Углеводы на 100г</param>
	public record Product
	{
		public Product( string name, float calories, float? proteins, float? fats, float? carbs, byte infoSourceType )
		{
			InfoSourceType = infoSourceType;
			Name = name;
			Calories = calories;
			Proteins = proteins;
			Fats = fats;
			Carbs = carbs;
		}

		public int Id { get; set; }
		public byte InfoSourceType { get; set; }
		public string Name { get; set; }
		public float Calories { get; set; }
		public float? Proteins { get; set; }
		public float? Fats { get; set; }
		public float? Carbs { get; set; }
	}
}
