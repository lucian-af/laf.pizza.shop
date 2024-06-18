namespace PizzaShop.API.Domain.Entities
{
	public sealed class CalculateDayOrdersAmount(decimal currentDayAmount, decimal yesterdayAmount)
	{
		public decimal Amount { get; } = currentDayAmount;

		public decimal DiffFromYesterday { get; } = currentDayAmount > 0 && yesterdayAmount > 0
			? Math.Round((currentDayAmount * 100 / yesterdayAmount) - 100, 2)
			: 0;
	}
}