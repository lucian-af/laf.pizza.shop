using PizzaShop.API.Domain.Entities;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IAuthLinkRepository : IRepository<AuthLink>
	{
		User GetUserFromEmail(string email);

		void AddAuthLink(AuthLink authLink);

		AuthLink GetAuthLinkFromCode(string code);

		void DeleteAuthLinkFromCode(string code);
	}
}