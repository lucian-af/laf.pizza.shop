using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.UseCases
{
	public sealed class CancelOrder(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<Guid>(authenticate, _resturantRequired: true)
	{
		public override async Task Execute(Guid data)
		{
			var order = await _orderRepository.GetOrderById(data, UserToken.RestaurantId.ToGuid())
				?? throw new NullValueException("Order not found.");

			order.Cancel();
			await _orderRepository.UnitOfWork.Commit();
		}
	}
}