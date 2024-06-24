using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.UseCases
{
	public sealed class GetMonthRevenue(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<CalculateMonthRevenue>(authenticate, _resturantRequired: true)
	{
		public override Task<Result<CalculateMonthRevenue>> Execute()
		{
			var startOfLastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
				.AddMonths(-1);
			var result = _orderRepository
				.GetMonthRevenue(UserToken.RestaurantId.ToGuid(), startOfLastMonth);

			var currentMonthWithYear = DateTime.Today.ToString("yyyy-MM");
			var lastMonthWithYear = startOfLastMonth.ToString("yyyy-MM");

			var currentMonthRevenue = result.FirstOrDefault(r => r.MonthWithYear == currentMonthWithYear);
			var lastMonthRevenue = result.FirstOrDefault(r => r.MonthWithYear == lastMonthWithYear);

			return Task.FromResult(new Result<CalculateMonthRevenue>
			{
				Data = new CalculateMonthRevenue(currentMonthRevenue?.Revenue ?? 0, lastMonthRevenue?.Revenue ?? 0)
			});
		}
	}
}