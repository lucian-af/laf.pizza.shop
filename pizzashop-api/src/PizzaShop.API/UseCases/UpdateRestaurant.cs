using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public sealed class UpdateRestaurant(IAuthenticate authenticate, IRestaurantRepository _restaurantRepository)
		: UseCaseBase<UpdateRestaurantDto>(authenticate)
	{
		public override async Task Execute(UpdateRestaurantDto data)
		{
			ArgumentNullException.ThrowIfNull(data, nameof(data));

			var restaurant = _restaurantRepository.GetResturantFromManager(UserToken.UserId.ToGuid())
				?? throw new NullValueException("Restaurant not found.");

			restaurant.UpdateProfile(data.Name, data.Description);
			await _restaurantRepository.UnitOfWork.Commit();
		}
	}
}