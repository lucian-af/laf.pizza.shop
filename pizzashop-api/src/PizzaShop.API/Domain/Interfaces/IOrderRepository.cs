using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Models;
using PizzaShop.API.Infrastructure.Repositories.Dao;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<Order> GetOrderDetailsById(Guid orderId);

		Task<Order> GetOrderById(Guid orderId);

		(IEnumerable<Order>, int) GetOrders(Guid restaurantId, Guid? orderId, OrderStatus? status, string customerName, int pageIndex = 0);

		IEnumerable<GetMonthRevenueDto> GetMonthRevenue(Guid restaurantId, DateTime date);

		IEnumerable<GetDayOrdersAmountDto> GetDayOrdersAmount(Guid restaurantId, DateTime date);

		IEnumerable<GetMonthOrdersAmountDto> GetMonthOrdersAmount(Guid restaurantId, DateTime date);

		IEnumerable<GetMonthCanceledOrdersAmountDto> GetMonthCanceledOrdersAmount(Guid restaurantId, DateTime date);

		IEnumerable<GetPopularProductsDto> GetPopularProducts(Guid restaurantId, int topPopular = 5);
	}
}