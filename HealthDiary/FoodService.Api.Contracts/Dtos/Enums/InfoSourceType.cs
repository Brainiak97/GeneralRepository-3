namespace FoodService.Api.Contracts.Dtos.Enums
{
	/// <summary>
	/// Тип источника информации
	/// Warn: должен совпадать с FoodService.DAL.Enums.InfoSourceType
	/// </summary>
	public enum InfoSourceType : byte
	{
		Default = 1,
		FromUser = 2,
	}
}
