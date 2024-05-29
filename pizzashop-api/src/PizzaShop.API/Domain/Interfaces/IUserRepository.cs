using PizzaShop.API.Domain.Entities.Authenticate;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		User GetUserById(Guid id);

		User GetUserFromEmail(string email);

		void AddAuthLink(AuthLink authLink);

		AuthLink GetAuthLinkFromCode(string code);

		void DeleteAuthLinkFromCode(string code);
	}
}