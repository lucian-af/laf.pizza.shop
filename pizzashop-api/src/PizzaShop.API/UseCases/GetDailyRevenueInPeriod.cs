using System.Globalization;
using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Infrastructure.Repositories.Dao;

namespace PizzaShop.API.UseCases
{
	public sealed class GetDailyRevenueInPeriod(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<GetDailyRevenueInPeriodFilterDto>(authenticate, _resturantRequired: true)
	{
		public override Task<Result<List<GetDailyRevenueInPeriodDto>>> Execute(GetDailyRevenueInPeriodFilterDto data)
		{
			var startDate = data.From ?? DateTime.Now.AddDays(-7);
			var endDate = data.To ?? (data.From.HasValue ? data.From.Value.AddDays(7) : DateTime.Now);

			if ((endDate - startDate).TotalDays > 7)
				throw new ArgumentException("You cannot list revenue in a larger period than 7 days.");

			var query = _orderRepository
				.GetDailyRevenueInPeriod(UserToken.RestaurantId.ToGuid(), startDate, endDate);

			var result = query.ToList();
			result.Sort((a, b) =>
			{
				var dayWithMonthASepareted = a.DayWithMonth.Split('/');
				var dayA = Convert.ToInt32(dayWithMonthASepareted[0]);
				var monthA = Convert.ToInt32(dayWithMonthASepareted[1]);

				var dayWithMonthBSepareted = b.DayWithMonth.Split('/');
				var dayB = Convert.ToInt32(dayWithMonthBSepareted[0]);
				var monthB = Convert.ToInt32(dayWithMonthBSepareted[1]);

				if (monthA == monthB)
					return dayB - dayA;
				else
				{
					var dateA = DateTime.ParseExact($"{monthA}/{dayA}", "M/d", CultureInfo.InvariantCulture);
					var dateB = DateTime.ParseExact($"{monthB}/{dayB}", "M/d", CultureInfo.InvariantCulture);
					return (int)(dateB - dateA).TotalMilliseconds;
				}
			});

			return Task.FromResult(new Result<List<GetDailyRevenueInPeriodDto>>
			{
				Data = result
			});
		}
	}
}