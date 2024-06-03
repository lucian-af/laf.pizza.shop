using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Domain.Exceptions;

namespace PizzaShop.API.Domain.Entities.Orders
{
	public class OrderItem(Guid orderId, Guid? productId, int quantity, decimal price) : Entity
	{
		public Guid OrderId { get; private set; } = orderId;
		public Guid? ProductId { get; private set; } = productId;
		public int Quantity { get; private set; } = quantity > 0 ? quantity : throw new DomainException("Quantity should be greather than zero.");
		public decimal Price { get; private set; } = price > 0 ? price : throw new DomainException("Price should be greather than zero.");

		public Order Order { get; private set; }
		public Product Product { get; private set; }
	}
}