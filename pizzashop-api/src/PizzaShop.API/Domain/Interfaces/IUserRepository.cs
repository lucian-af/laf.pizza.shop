using PizzaShop.API.Domain.Entities;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		User GetUserById(Guid id);
	}
}