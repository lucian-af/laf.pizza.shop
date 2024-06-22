using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.UseCases
{
	public sealed class GetMonthCanceledOrdersAmount(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<CalculateMonthCanceledOrdersAmount>(authenticate, true)
	{
		public override Task<Result<CalculateMonthCanceledOrdersAmount>> Execute()
		{
			var startOfLastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
				.AddMonths(-1);
			var ordersPerMonth = _orderRepository
				.GetMonthCanceledOrdersAmount(UserToken.RestaurantId.ToGuid(), startOfLastMonth);

			var currentMonthWithYear = DateTime.Today.ToString("yyyy-MM");
			var lastMonthWithYear = startOfLastMonth.ToString("yyyy-MM");

			var currentMonthAmount = ordersPerMonth.FirstOrDefault(r => r.MonthWithYear == currentMonthWithYear);
			var lastMonthAmount = ordersPerMonth.FirstOrDefault(r => r.MonthWithYear == lastMonthWithYear);

			return Task.FromResult(new Result<CalculateMonthCanceledOrdersAmount>
			{
				Data = new CalculateMonthCanceledOrdersAmount(currentMonthAmount?.Amount ?? 0, lastMonthAmount?.Amount ?? 0)
			});
		}
	}
}