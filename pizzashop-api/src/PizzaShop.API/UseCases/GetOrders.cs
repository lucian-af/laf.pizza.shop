﻿using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public sealed class GetOrders(IAuthenticate authenticate, IOrderRepository _orderRepository)
		: UseCaseBase<GetOrdersFiltersDto>(authenticate, _resturantRequired: true)
	{
		public override Task<Result<GetOrdersDto>> Execute(GetOrdersFiltersDto filters)
		{
			var (orders, total) = _orderRepository.GetOrders(
				UserToken.RestaurantId.ToGuid(),
				filters.OrderId,
				filters.Status,
				filters.CustomerName,
				filters.PageIndex);

			var result = new GetOrdersDto();
			foreach (var order in orders)
			{
				result.Orders.Add(new()
				{
					OrderId = order.Id,
					Status = order.Status,
					Total = order.Total,
					CreatedAt = order.CreatedAt,
					CustomerName = order.Customer.Name,
				});
			}

			result.TotalCount = total;
			result.PageIndex = filters.PageIndex;
			result.PerPage = result.Orders.Count > 10 ? 10 : result.Orders.Count;

			return Task.FromResult(new Result<GetOrdersDto> { Data = result });
		}
	}
}