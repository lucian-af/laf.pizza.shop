using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Exceptions;

namespace PizzaShop.API.Domain.Entities.Shops
{
	public class Product : Auditable
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public decimal Price { get; private set; }
		public Guid RestaurantId { get; private set; }

		public Restaurant Restaurant { get; private set; }
		public List<OrderItem> OrderItems { get; private set; }

		public Product(string name, string description, decimal price, Guid restaurantId)
		{
			Name = name;
			Description = description;
			Price = price;
			RestaurantId = restaurantId;
		}

		public void ChangePrice(decimal newPrice)
		{
			if (newPrice <= 0)
				throw new DomainException("Price should greater than zero.");

			Price = newPrice;
		}

		public void ChangeName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new DomainException("Name is required.");

			Name = name;
		}

		public void ChangeDescription(string description)
			=> Description = description;
	}
}