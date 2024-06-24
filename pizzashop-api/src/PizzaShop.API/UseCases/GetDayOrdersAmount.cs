using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.UseCases
{
	public sealed class GetDayOrdersAmount(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<CalculateDayOrdersAmount>(authenticate, _resturantRequired: true)
	{
		public override Task<Result<CalculateDayOrdersAmount>> Execute()
		{
			var yesterday = DateTime.Today.AddDays(-1);
			var ordersPerDay = _orderRepository
				.GetDayOrdersAmount(UserToken.RestaurantId.ToGuid(), yesterday);

			var currentDay = DateTime.Today.ToString("yyyy-MM-dd");

			var todayOrdersAmount = ordersPerDay.FirstOrDefault(r => r.DayWithMonthAndYear == currentDay);
			var yesterdayOrdersAmount = ordersPerDay.FirstOrDefault(r => r.DayWithMonthAndYear == yesterday.ToString("yyyy-MM-dd"));

			return Task.FromResult(new Result<CalculateDayOrdersAmount>
			{
				Data = new CalculateDayOrdersAmount(todayOrdersAmount?.Amount ?? 0, yesterdayOrdersAmount?.Amount ?? 0)
			});
		}
	}
}