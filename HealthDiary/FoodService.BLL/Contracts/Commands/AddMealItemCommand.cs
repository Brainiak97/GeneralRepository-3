namespace FoodService.BLL.Contracts.Commands
{
	/// <summary>
	/// Команда добавления элемента приёма пищи
	/// </summary>
	/// <param name="MealId">Приём пищи, для которого добавляется элемент</param>
	/// <param name="ProductId">Потребляемый продукт</param>
	/// <param name="Quantity">Количество продукта, г</param>
	public record AddMealItemCommand(
		int MealId,
		int ProductId,
		float Quantity );
}
