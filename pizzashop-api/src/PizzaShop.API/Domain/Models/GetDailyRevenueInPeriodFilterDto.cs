namespace PizzaShop.API.Infrastructure.Repositories.Dao
{
	public class GetDailyRevenueInPeriodFilterDto
	{
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
	}
}