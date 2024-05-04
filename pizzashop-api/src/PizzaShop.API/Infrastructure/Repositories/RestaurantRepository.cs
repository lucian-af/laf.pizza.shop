using PizzaShop.API.Domain;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Infrastructure.Context;
using PizzaShop.API.Infrastructure.Data;

namespace PizzaShop.API.Infrastructure.Repositories
{
	public class RestaurantRepository(PizzaShopContext context) : IRestaurantRepository
	{
		private readonly PizzaShopContext _context = context;

		public IUnitOfWork UnitOfWork => _context;

		public void AddResturant(Restaurant restaurant)
		{
			_context.Users.Add(restaurant.Manager);
			_context.Restaurants.Add(restaurant);
		}

		#region Disposible

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
			=> _context.Dispose();

		~RestaurantRepository()
			=> Dispose(false);

		#endregion Disposible
	}
}