using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Infrastructure.Context;
using PizzaShop.API.Infrastructure.Data;
using PizzaShop.API.Infrastructure.Repositories.Dao;

namespace PizzaShop.API.Infrastructure.Repositories
{
	public class OrderRepository(PizzaShopContext context) : IOrderRepository
	{
		private readonly PizzaShopContext _context = context;

		public IUnitOfWork UnitOfWork => _context;

		public Task<Order> GetOrderDetailsById(Guid orderId)
			=> _context.Orders.Where(o => o.Id.Equals(orderId))
							  .AsNoTracking()
							  .Include(s => s.Customer)
							  .Include(s => s.OrderItems)
							  .ThenInclude(s => s.Product)
							  .FirstOrDefaultAsync();

		public Task<Order> GetOrderById(Guid orderId)
					=> _context.Orders.FindAsync(orderId).AsTask();

		public (IEnumerable<Order>, int) GetOrders(Guid restaurantId, Guid? orderId, OrderStatus? status, string customerName, int pageIndex = 0)
		{
			var query = _context.Orders
				.AsNoTracking()
				.Include(od => od.Restaurant)
				.Include(od => od.Customer)
				.Include(od => od.OrderItems)
				.ThenInclude(oi => oi.Product)
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

		public IEnumerable<GetMonthRevenueDto> GetMonthRevenue(Guid restaurantId, DateTime month)
		{
			var monthConvert = new DateTimeOffset(month).UtcDateTime;
			return _context.Database.SqlQuery<GetMonthRevenueDto>(@$"
								SELECT to_char(o.""createdAt"", 'YYYY-MM') AS monthWithYear,
									   sum(o.total) AS revenue
								  FROM orders o
								 WHERE o.""restaurantId"" = {restaurantId}
								   AND o.""createdAt"" >= {monthConvert}
								 GROUP BY to_char(o.""createdAt"", 'YYYY-MM')");
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