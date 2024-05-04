﻿using PizzaShop.API.Domain.Enums;
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
		public Guid? ManagerId { get; private set; }

		public User Manager { get; private set; }

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

			if (newManager.Role != Enums.RoleUser.Manager)
				throw new DomainException("User role type not allowed.");

			Manager = newManager;
			ManagerId = newManager.Id;
		}
	}
}