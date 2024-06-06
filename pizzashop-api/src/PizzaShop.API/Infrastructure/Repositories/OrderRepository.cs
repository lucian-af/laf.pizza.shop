using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Infrastructure.Context;
using PizzaShop.API.Infrastructure.Data;

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