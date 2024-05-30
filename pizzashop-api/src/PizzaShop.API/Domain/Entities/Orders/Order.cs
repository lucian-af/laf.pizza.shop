using PizzaShop.API.Domain.Entities.Authenticate;
using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.Domain.Entities.Orders
{
	public class Order : Auditable, IAggregateRoot
	{
		public Guid? CustomerId { get; private set; }
		public Guid RestaurantId { get; private set; }
		public OrderStatus Status { get; private set; } = OrderStatus.Pending;
		public decimal ValorTotal { get; private set; }

		public User Customer { get; private set; }
		public Restaurant Restaurant { get; private set; }
		public List<OrderItem> OrderItems { get; private set; }
	}
}