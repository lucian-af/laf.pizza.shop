using PizzaShop.API.Domain.Models;
using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class OrderEndpoints
	{
		public static IEndpointRouteBuilder UseOrderEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapGet("/orders/details/{orderId}", async (Guid orderId, GetOrderDetails getOrderDetails) =>
			{
				var result = await getOrderDetails.Execute(orderId);
				return Results.Ok(result.Data);
			}).WithOpenApi();

			groupBuilder.MapPatch("/orders/{orderId}/aprove", async (Guid orderId, AproveOrder aproveOrder) =>
			{
				await aproveOrder.Execute(orderId);
				return Results.Ok();
			}).WithOpenApi();

			groupBuilder.MapPatch("/orders/{orderId}/cancel", async (Guid orderId, CancelOrder cancelOrder) =>
			{
				await cancelOrder.Execute(orderId);
				return Results.Ok();
			}).WithOpenApi();

			groupBuilder.MapPatch("/orders/{orderId}/deliver", async (Guid orderId, DeliverOrder deliverOrder) =>
			{
				await deliverOrder.Execute(orderId);
				return Results.Ok();
			}).WithOpenApi();

			groupBuilder.MapPatch("/orders/{orderId}/dispatch", async (Guid orderId, DispatchOrder dispatchOrder) =>
			{
				await dispatchOrder.Execute(orderId);
				return Results.Ok();
			}).WithOpenApi();

			groupBuilder.MapPost("/orders", async (GetOrdersFiltersDto filters, GetOrders getOrders) =>
			{
				var result = await getOrders.Execute(filters);
				return Results.Ok(result.Data);
			}).WithOpenApi();

			return app;
		}
	}
}