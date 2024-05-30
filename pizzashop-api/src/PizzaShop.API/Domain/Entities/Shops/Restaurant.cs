using PizzaShop.API.Domain.Entities.Authenticate;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.Domain.Entities.Shops
{
	public sealed class Restaurant : Auditable, IAggregateRoot
	{
		// EF Core
		private Restaurant()
		{ }

		public string Name { get; private set; }
		public string Description { get; private set; }
		public Guid? ManagerId { get; private set; }

		public User Manager { get; private set; }
		public List<Product> Products { get; private set; }
		public List<Order> Orders { get; private set; }

		public Restaurant(string name, string description, string managerName, string email, string phone)
		{
			Name = name;
			Description = description;
			Manager = new User(managerName, email, phone, RoleUser.Manager);
			ManagerId = Manager.Id;
		}

		public void ChangeManager(User newManager)
		{
			if (newManager is null)
				throw new DomainException("User is required.");

			if (newManager.Role != RoleUser.Manager)
				throw new DomainException("User role type not allowed.");

			Manager = newManager;
			ManagerId = newManager.Id;
		}

		public void UpdateProfile(string name, string description)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new DomainException("Name is required.");

			Name = name;
			Description = string.IsNullOrWhiteSpace(description) ? null : description;
		}
	}
}