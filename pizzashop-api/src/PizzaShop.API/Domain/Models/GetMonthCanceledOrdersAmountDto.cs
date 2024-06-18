namespace PizzaShop.API.Infrastructure.Repositories.Dao
{
	public class GetMonthCanceledOrdersAmountDto
	{
		public string MonthWithYear { get; set; }
		public int Amount { get; set; }
	}
}