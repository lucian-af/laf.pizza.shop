using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class AddRestaurant(IRestaurantRepository restaurantRepository) : UseCaseBase<AddRestaurantDto>
	{
		private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

		public override async Task Execute(AddRestaurantDto data)
		{
			var restaurant = new Restaurant(
				 data.RestaurantName,
				   description: null,
				   data.Name,
					 data.Email,
					  data.Phone);

			_restaurantRepository.AddResturant(restaurant);
			var success = await _restaurantRepository.UnitOfWork.Commit();

			if (!success)
				throw new Exception("Error on add restaurant.");
		}
	}
}