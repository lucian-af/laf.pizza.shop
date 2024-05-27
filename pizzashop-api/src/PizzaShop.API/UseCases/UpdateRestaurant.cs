using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class UpdateRestaurant(
		IAuthenticate authenticate,
		IRestaurantRepository restaurantRepository) : UseCaseBase<UpdateRestaurantDto>
	{
		private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
		private readonly IAuthenticate _authenticate = authenticate;

		public override async Task Execute(UpdateRestaurantDto data)
		{
			ArgumentNullException.ThrowIfNull(data, nameof(data));

			var payload = _authenticate.GetPayload<UserTokenDto>()
				?? throw new ArgumentException("Unauthorized.");

			var restaurant = _restaurantRepository.GetResturantFromManager(payload.UserId.ToGuid())
				?? throw new NullValueException("Restaurant not found.");

			restaurant.UpdateProfile(data.Name, data.Description);
			await _restaurantRepository.UnitOfWork.Commit();
		}
	}
}