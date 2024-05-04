namespace PizzaShop.API.Domain
{
	public abstract class Auditable : Entity
	{
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}