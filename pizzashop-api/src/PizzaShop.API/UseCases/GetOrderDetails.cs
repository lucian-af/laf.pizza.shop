using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;
using static PizzaShop.API.Domain.Models.GetOrderDetailsDto;

namespace PizzaShop.API.UseCases
{
	public class GetOrderDetails(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<Guid>(authenticate)
	{
		public override async Task<Result<GetOrderDetailsDto>> Execute(Guid data)
		{
			var order = await _orderRepository.GetOrderDetailsById(data)
				?? throw new NullValueException("Order not found.");

			var result = new GetOrderDetailsDto
			{
				OrderId = order.Id,
				CreatedAt = order.CreatedAt,
				Status = order.Status.ToString(),
				Total = order.Total,
				CustomerName = order.Customer?.Name,
				CustomerEmail = order.Customer?.Email,
				CustomerPhone = order.Customer?.Phone,
				OrderItems = order.OrderItems?.Select(orderItem => new OrderItemsDetails
				{
					OrderItemId = orderItem.Id,
					Price = orderItem.Price,
					ProductName = orderItem.Product.Name,
					Quantity = orderItem.Quantity,
				})?.ToList() ?? []
			};

			return new Result<GetOrderDetailsDto> { Data = result };
		}
	}
}