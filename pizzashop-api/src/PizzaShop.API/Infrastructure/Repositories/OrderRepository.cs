using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;
using PizzaShop.API.Infrastructure.Context;
using PizzaShop.API.Infrastructure.Data;
using PizzaShop.API.Infrastructure.Repositories.Dao;

namespace PizzaShop.API.Infrastructure.Repositories
{
	public class OrderRepository(PizzaShopContext context) : IOrderRepository
	{
		private readonly PizzaShopContext _context = context;

		public IUnitOfWork UnitOfWork => _context;

		public Task<Order> GetOrderDetailsById(Guid orderId, Guid restaurantId)
			=> _context.Orders.Where(o => o.Id.Equals(orderId) && o.RestaurantId.Equals(restaurantId))
							  .AsNoTracking()
							  .Include(s => s.Customer)
							  .Include(s => s.OrderItems)
							  .ThenInclude(s => s.Product)
							  .FirstOrDefaultAsync();

		public Task<Order> GetOrderById(Guid orderId, Guid restaurantId)
					=> _context.Orders.FirstOrDefaultAsync(o => o.Id.Equals(orderId) && o.RestaurantId.Equals(restaurantId));

		public (IEnumerable<Order>, int) GetOrders(Guid restaurantId, Guid? orderId, OrderStatus? status, string customerName, int pageIndex = 0)
		{
			var query = _context.Orders
				.AsNoTracking()
				.Include(od => od.Restaurant)
				.Include(od => od.Customer)
				.Include(od => od.OrderItems)
				.Where(od => od.RestaurantId.Equals(restaurantId));

			if (orderId is not null)
				query = query.Where(od => od.Id.Equals(orderId));

			if (status is not null)
				query = query.Where(od => od.Status.Equals(status));

			if (!string.IsNullOrWhiteSpace(customerName))
				query = query.Where(od => EF.Functions.Like(od.Customer.Name.ToLower(), $"%{customerName.ToLower()}%"));

			query = query
				.OrderBy(od => od.Status)
				.ThenByDescending(od => od.CreatedAt);

			var totalOrders = query.Count();
			query = query.Skip(pageIndex * 10).Take(10);

			return (query, totalOrders);
		}

		public IEnumerable<GetMonthRevenueDto> GetMonthRevenue(Guid restaurantId, DateTime date)
		{
			var monthConvert = new DateTimeOffset(date).UtcDateTime;
			return _context.Database.SqlQuery<GetMonthRevenueDto>(@$"
								SELECT to_char(o.""createdAt"", 'YYYY-MM') AS monthWithYear,
									   sum(o.total) AS revenue
								  FROM orders o
								 WHERE o.""restaurantId"" = {restaurantId}
								   AND o.""createdAt"" >= {monthConvert}
								 GROUP BY to_char(o.""createdAt"", 'YYYY-MM')");
		}

		public IEnumerable<GetDayOrdersAmountDto> GetDayOrdersAmount(Guid restaurantId, DateTime date)
		{
			var monthConvert = new DateTimeOffset(date).UtcDateTime.ToString("yyyy-MM-dd");
			return _context.Database.SqlQuery<GetDayOrdersAmountDto>(@$"
								SELECT to_char(o.""createdAt"", 'YYYY-MM-DD') AS dayWithMonthAndYear,
									   count(o.id) AS amount
								  FROM orders o
								 WHERE o.""restaurantId"" = {restaurantId}
								   AND to_char(o.""createdAt"", 'YYYY-MM-DD') >= {monthConvert}
								 GROUP BY to_char(o.""createdAt"", 'YYYY-MM-DD')");
		}

		public IEnumerable<GetMonthOrdersAmountDto> GetMonthOrdersAmount(Guid restaurantId, DateTime date)
		{
			var monthConvert = new DateTimeOffset(date).UtcDateTime;
			return _context.Database.SqlQuery<GetMonthOrdersAmountDto>(@$"
								SELECT to_char(o.""createdAt"", 'YYYY-MM') AS monthWithYear,
									   count(o.id) AS amount
								  FROM orders o
								 WHERE o.""restaurantId"" = {restaurantId}
								   AND o.""createdAt"" >= {monthConvert}
								 GROUP BY to_char(o.""createdAt"", 'YYYY-MM')");
		}

		public IEnumerable<GetMonthCanceledOrdersAmountDto> GetMonthCanceledOrdersAmount(Guid restaurantId, DateTime date)
		{
			var monthConvert = new DateTimeOffset(date).UtcDateTime;
			return _context.Database.SqlQuery<GetMonthCanceledOrdersAmountDto>(@$"
								SELECT to_char(o.""createdAt"", 'YYYY-MM') AS monthWithYear,
									   count(o.id) AS amount
								  FROM orders o
								 WHERE o.""restaurantId"" = {restaurantId}
								   AND o.""createdAt"" >= {monthConvert}
								   AND o.status = {(int)OrderStatus.Canceled}
								 GROUP BY to_char(o.""createdAt"", 'YYYY-MM')");
		}

		public IEnumerable<GetPopularProductsDto> GetPopularProducts(Guid restaurantId, int topPopular = 5)
		{
			var query = _context.Database
				.SqlQuery<GetPopularProductsDto>(@$"
						SELECT p.""name"" AS product, sum(oi.quantity) AS amount
						  FROM ""orderItems""oi
                    INNER JOIN orders o ON o.id = oi.""orderId""
					 LEFT JOIN products p ON p.id = oi.""productId""
					     WHERE o.""restaurantId"" = {restaurantId}
					  GROUP BY p.""name""
					  ORDER BY amount DESC
						 LIMIT {topPopular}");

			return query;
		}

		public IEnumerable<GetDailyRevenueInPeriodDto> GetDailyRevenueInPeriod(Guid restaurantId, DateTime startDate, DateTime endDate)
		{
			var startDateConvert = new DateTimeOffset(startDate).UtcDateTime;
			var endDateConvert = new DateTimeOffset(endDate).UtcDateTime;

			var query = _context.Database
				.SqlQuery<GetDailyRevenueInPeriodDto>(@$"
						SELECT to_char(o.""createdAt"", 'DD/MM') AS dayWithMonth,
    						   sum(o.total) AS revenue
						  FROM orders o
						 WHERE o.""restaurantId"" = {restaurantId}
						   AND o.""createdAt"" BETWEEN {startDateConvert} AND {endDateConvert}
					  GROUP BY to_char(o.""createdAt"", 'DD/MM')
					");

			return query;
		}

		#region Disposible

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
			=> _context.Dispose();

		~OrderRepository()
			=> Dispose(false);

		#endregion Disposible
	}
}