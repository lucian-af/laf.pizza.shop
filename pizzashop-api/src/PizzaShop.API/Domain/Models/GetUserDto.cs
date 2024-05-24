using System.Text.Json.Serialization;
using PizzaShop.API.Domain.Enums;

namespace PizzaShop.API.Domain.Models
{
	public class GetUserDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public RoleUser Role { get; set; }
	}
}