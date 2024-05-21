using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class GetManagedRestaurant(
		IAuthenticate authenticate,
		IRestaurantRepository restaurantRepository) : UseCaseBase<GetRestaurantDto>
	{
		private readonly IAuthenticate _authenticate = authenticate;
		private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

		public override Task<Result<GetRestaurantDto>> Execute()
		{
			var payload = _authenticate.GetPayload<UserTokenDto>()
				?? throw new ArgumentException("Unauthorized.");

			if (payload.RestaurantId is null)
				throw new ArgumentException("User is not manager.");

			var restaurant = _restaurantRepository.GetResturantById(payload.RestaurantId.ToGuid());

			if (payload.RestaurantId is null)
				throw new NotFoundException("Restaurant not found.");

			return Task.FromResult(new Result<GetRestaurantDto>
			{
				Data = new()
				{
					Id = restaurant.Id,
					Description = restaurant?.Description,
					Name = restaurant?.Name
				}
			});
		}
	}
}