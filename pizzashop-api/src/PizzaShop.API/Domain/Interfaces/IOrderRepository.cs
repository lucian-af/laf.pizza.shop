using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Enums;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<Order> GetOrderDetailsById(Guid orderId);

		Task<Order> GetOrderById(Guid orderId);

		(IEnumerable<Order>, int) GetOrders(Guid restaurantId, Guid? orderId, OrderStatus? status, string customerName, int pageIndex = 0);
	}
}