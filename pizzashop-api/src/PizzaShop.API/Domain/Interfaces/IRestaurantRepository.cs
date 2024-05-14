using PizzaShop.API.Domain.Entities;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IRestaurantRepository : IRepository<Restaurant>
	{
		void AddResturant(Restaurant restaurant);

		Restaurant GetResturantFromManager(Guid managerId);
	}
}