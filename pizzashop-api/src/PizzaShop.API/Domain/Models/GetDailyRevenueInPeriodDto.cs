namespace PizzaShop.API.Infrastructure.Repositories.Dao
{
	public class GetDailyRevenueInPeriodDto
	{
		public string DayWithMonth { get; set; }
		public decimal Revenue { get; set; }
	}
}