using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public sealed class GetManagedRestaurant(IAuthenticate authenticate, IRestaurantRepository _restaurantRepository)
		: UseCaseBase<GetRestaurantDto>(authenticate)
	{
		public override Task<Result<GetRestaurantDto>> Execute()
		{
			UnauthorizedException.ThrowIfNullOrWhiteSpace(UserToken.RestaurantId, "User is not manager.");

			var restaurant = _restaurantRepository.GetResturantById(UserToken.RestaurantId.ToGuid())
				?? throw new NullValueException("Restaurant not found.");

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