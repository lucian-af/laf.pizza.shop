namespace PizzaShop.API.Domain.Entities
{
	public sealed class CalculateMonthRevenue(decimal currentMonthRevenue, decimal lastMonthRevenue)
	{
		public decimal Revenue { get; } = currentMonthRevenue;

		public decimal DiffFromLastMonth { get; } = currentMonthRevenue > 0 && lastMonthRevenue > 0
			? Math.Round((currentMonthRevenue * 100 / lastMonthRevenue) - 100, 2)
			: 0;
	}
}