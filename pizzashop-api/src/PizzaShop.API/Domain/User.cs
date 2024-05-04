using PizzaShop.API.Domain.Enums;

namespace PizzaShop.API.Domain
{
	public partial class User(string name, string email, string phone, RoleUser role = RoleUser.Customer) : Auditable
	{
		public string Name { get; } = name;
		public string Email { get; private set; } = email.ToLower();
		public string Phone { get; private set; } = StringValidations.ClearSpecialCharacters().Replace(phone, "");
		public RoleUser Role { get; } = role;

		public virtual List<Restaurant> Restaurants { get; }

		public void ChangePhone(string newPhone)
			=> Phone = newPhone;

		public void ChangeEmail(string newEmail)
			=> Email = newEmail;
	}
}
