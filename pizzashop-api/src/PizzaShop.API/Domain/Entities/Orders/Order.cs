using PizzaShop.API.Domain.Entities.Authenticate;
using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.Domain.Entities.Orders
{
	public class Order(Guid? customerId, Guid restaurantId) : Auditable, IAggregateRoot
	{
		public Guid? CustomerId { get; private set; } = customerId;
		public Guid RestaurantId { get; private set; } = restaurantId;
		public OrderStatus Status { get; private set; } = OrderStatus.Pending;

		public decimal Total
		{
			get => OrderItems.Sum(oi => oi.Quantity * oi.Price);
			private set { }
		}

		public User Customer { get; private set; }
		public Restaurant Restaurant { get; private set; }
		public List<OrderItem> OrderItems { get; private set; } = [];

		private bool OrderUnavailable => Status is OrderStatus.Canceled or OrderStatus.Delivered or OrderStatus.Delivering;

		public void AddItem(Guid productId, int quantity, decimal price)
		{
			if (OrderUnavailable)
				throw new DomainException("Order unavailable.");

			var orderItem = new OrderItem(Id, productId, quantity, price);
			OrderItems.Add(orderItem);
		}

		public void Approve()
		{
			if (Status != OrderStatus.Pending)
				throw new DomainException("Order is not pending.");

			Status = OrderStatus.Processing;
		}

		public void Dispatch()
		{
			if (Status != OrderStatus.Processing)
				throw new DomainException($"Status change not allowed. Order in: {Status}");

			Status = OrderStatus.Delivering;
		}

		public void Deliver()
		{
			if (Status != OrderStatus.Delivering)
				throw new DomainException($"Status change not allowed. Order in: {Status}");

			Status = OrderStatus.Delivered;
		}

		public void Cancel()
		{
			if (Status is OrderStatus.Pending or OrderStatus.Processing)
				Status = OrderStatus.Canceled;
			else
				throw new DomainException($"Status change not allowed. Order in: {Status}");
		}

		// TODO: rever
		public void ChangeStatus(OrderStatus newStatus)
		{
			if (OrderUnavailable)
				throw new DomainException($"Status change not allowed. Order: {Status}");

			Status = newStatus;
		}
	}
}