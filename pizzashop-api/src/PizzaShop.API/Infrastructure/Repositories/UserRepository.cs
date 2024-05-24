using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Infrastructure.Context;
using PizzaShop.API.Infrastructure.Data;

namespace PizzaShop.API.Infrastructure.Repositories
{
	public class UserRepository(PizzaShopContext context) : IUserRepository
	{
		private readonly PizzaShopContext _context = context;

		public IUnitOfWork UnitOfWork => _context;

		public User GetUserById(Guid id)
			=> _context.Users.Find(id);

		#region Disposible

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
			=> _context.Dispose();

		~UserRepository()
			=> Dispose(false);

		#endregion Disposible
	}
}