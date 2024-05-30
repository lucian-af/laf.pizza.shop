using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.Domain.Entities.Authenticate
{
	public class User : Auditable, IAggregateRoot
	{
		public string Name { get; }
		public string Email { get; private set; }
		public string Phone { get; private set; }
		public RoleUser Role { get; }

		public virtual List<Restaurant> Restaurants { get; }
		public virtual List<Order> Orders { get; }

		public User(string name, string email, string phone, RoleUser role = RoleUser.Customer)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new DomainException("Name is required.");

			if (string.IsNullOrWhiteSpace(email))
				throw new DomainException("E-mail is required.");

			if (string.IsNullOrWhiteSpace(phone))
				throw new DomainException("Phone is required.");

			Name = name;
			Email = email.ToLower();
			Phone = StringValidations.ClearSpecialCharacters().Replace(phone, "");
			Role = role;
		}

		public void ChangePhone(string newPhone)
			=> Phone = newPhone;

		public void ChangeEmail(string newEmail)
			=> Email = newEmail;
	}
}