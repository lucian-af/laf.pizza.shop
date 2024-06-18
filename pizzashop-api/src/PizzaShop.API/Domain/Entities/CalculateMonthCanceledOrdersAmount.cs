namespace PizzaShop.API.Domain.Entities
{
	public sealed class CalculateMonthCanceledOrdersAmount(int currentMonthAmount, int lastMonthAmount)
	{
		public int Amount { get; } = currentMonthAmount;

		public decimal DiffFromLastMonth { get; } = currentMonthAmount > 0 && lastMonthAmount > 0
			? (currentMonthAmount * 100 / lastMonthAmount) - 100
			: 0;
	}
}