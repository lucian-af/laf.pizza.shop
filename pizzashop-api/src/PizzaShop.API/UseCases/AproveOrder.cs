using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.UseCases
{
	public sealed class AproveOrder(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<Guid>(authenticate, _resturantRequired: true)
	{
		public override async Task Execute(Guid data)
		{
			var order = await _orderRepository.GetOrderById(data)
				?? throw new NullValueException("Order not found.");

			order.Aprove();
			await _orderRepository.UnitOfWork.Commit();
		}
	}
}