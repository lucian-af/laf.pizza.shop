using System.Text.Json.Serialization;
using PizzaShop.API.Domain.Enums;

namespace PizzaShop.API.Domain.Models
{
	public class GetOrdersDto
	{
		public List<OrderDetailsDto> Orders { get; set; } = [];
		public int TotalCount { get; set; }
		public int PerPage { get; set; }
		public int PageIndex { get; set; }

		public class OrderDetailsDto
		{
			public Guid OrderId { get; set; }
			[JsonConverter(typeof(JsonStringEnumConverter))]
			public OrderStatus Status { get; set; }
			public decimal Total { get; set; }
			public DateTime CreatedAt { get; set; }
			public string CustomerName { get; set; }
		}
	}
}