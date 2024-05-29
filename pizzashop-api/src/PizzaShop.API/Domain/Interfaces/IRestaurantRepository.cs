using PizzaShop.API.Domain.Entities.Shops;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IRestaurantRepository : IRepository<Restaurant>
	{
		void AddResturant(Restaurant restaurant);

		Restaurant GetResturantFromManager(Guid managerId);

		Restaurant GetResturantById(Guid restaurantId);
	}
}