namespace PizzaShop.API.Domain.Models
{
	public class GetRestaurantDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}