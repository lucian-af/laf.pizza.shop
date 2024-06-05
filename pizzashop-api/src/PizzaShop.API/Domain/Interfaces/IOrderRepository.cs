using PizzaShop.API.Domain.Entities.Orders;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<Order> GetOrderDetailsById(Guid orderId);
	}
}