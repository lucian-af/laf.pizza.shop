using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public sealed class GetPopularProducts(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<List<GetPopularProductsDto>>(authenticate)
	{
		public override Task<Result<List<GetPopularProductsDto>>> Execute()
		{
			UnauthorizedException.ThrowIfNullOrWhiteSpace(UserToken.RestaurantId);

			var products = _orderRepository.GetPopularProducts(UserToken.RestaurantId.ToGuid());

			return Task.FromResult(new Result<List<GetPopularProductsDto>> { Data = products.ToList() });
		}
	}
}