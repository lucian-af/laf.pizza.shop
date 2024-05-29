using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Domain.Entities.Authenticate;
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

		public User GetUserFromEmail(string email)
					=> _context.Users.AsNoTracking().SingleOrDefault(s => s.Email.Equals(email.ToLower()));

		public void AddAuthLink(AuthLink authLink)
			=> _context.AuthLinks.Add(authLink);

		public AuthLink GetAuthLinkFromCode(string code)
			=> _context.AuthLinks.FirstOrDefault(al => al.Code.Equals(code));

		public void DeleteAuthLinkFromCode(string code)
			=> _context.AuthLinks.Remove(_context.AuthLinks.FirstOrDefault(al => al.Code.Equals(code)));

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