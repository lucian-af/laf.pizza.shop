namespace PizzaShop.API.Domain.Entities
{
	public sealed class CalculateDayOrdersAmount(int currentDayAmount, int yesterdayAmount)
	{
		public int Amount { get; } = currentDayAmount;

		public decimal DiffFromYesterday { get; } = currentDayAmount > 0 && yesterdayAmount > 0
			? (currentDayAmount * 100 / yesterdayAmount) - 100
			: 0;
	}
}