namespace PizzaShop.API.Infrastructure.Repositories.Dao
{
	public class GetDayOrdersAmountDto
	{
		public string DayWithMonthAndYear { get; set; }
		public decimal Amount { get; set; }
	}
}