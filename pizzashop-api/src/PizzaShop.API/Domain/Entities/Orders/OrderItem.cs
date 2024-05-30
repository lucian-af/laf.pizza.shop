using PizzaShop.API.Domain.Entities.Shops;

namespace PizzaShop.API.Domain.Entities.Orders
{
	public class OrderItem : Entity
	{
		public Guid OrderId { get; private set; }
		public Guid? ProductId { get; private set; }
		public int Quantity { get; private set; }
		public decimal Price { get; private set; }

		public Order Order { get; private set; }
		public Product Product { get; private set; }
	}
}