using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Domain.Entities.Shops;
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

		public Restaurant GetResturantFromManager(Guid managerId)
			=> _context.Restaurants
					   .Include(r => r.Manager)
					   .FirstOrDefault(rs => rs.ManagerId == managerId);

		public Restaurant GetResturantById(Guid restaurantId)
			=> _context.Restaurants.Find(restaurantId);

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