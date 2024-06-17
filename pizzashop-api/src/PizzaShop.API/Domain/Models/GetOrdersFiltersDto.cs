using System.Text.Json.Serialization;
using PizzaShop.API.Domain.Enums;

namespace PizzaShop.API.Domain.Models
{
	public class GetOrdersFiltersDto
	{
		public Guid? OrderId { get; set; }
		public string CustomerName { get; set; }
		public int PageIndex { get; set; } = 0;

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public OrderStatus? Status { get; set; }
	}
}