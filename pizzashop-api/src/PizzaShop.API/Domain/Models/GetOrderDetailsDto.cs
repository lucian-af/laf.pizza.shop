namespace PizzaShop.API.Domain.Models
{
	public sealed class GetOrderDetailsDto
	{
		public Guid OrderId { get; set; }
		public string Status { get; set; }
		public decimal Total { get; set; }
		public DateTime CreatedAt { get; set; }
		public string CustomerName { get; set; }
		public string CustomerPhone { get; set; }
		public string CustomerEmail { get; set; }
		public List<OrderItemsDetails> OrderItems { get; set; } = [];

		public class OrderItemsDetails
		{
			public Guid OrderItemId { get; set; }
			public string ProductName { get; set; }
			public decimal Price { get; set; }
			public int Quantity { get; set; }
		}
	}
}