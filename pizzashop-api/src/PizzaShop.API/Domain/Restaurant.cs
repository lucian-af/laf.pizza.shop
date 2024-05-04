using PizzaShop.API.Domain.Exceptions;

namespace PizzaShop.API.Domain
{
	public sealed class Restaurant : Auditable
	{
		// EF Core
		private Restaurant()
		{ }

		public string Name { get; }
		public string Description { get; }
		public Guid? ManagerId { get; }

		public User Manager { get; }

		public Restaurant(string name, string description, User manager)
		{
			Name = name;
			Description = description;
			ManagerId = ValidManagerRole(manager);
		}

		private static Guid? ValidManagerRole(User manager)
		{
			if (manager is not null && manager.Role != Enums.RoleUser.Manager)
				throw new DomainException("User role type not allowed.");

			return manager?.Id;
		}
	}
}