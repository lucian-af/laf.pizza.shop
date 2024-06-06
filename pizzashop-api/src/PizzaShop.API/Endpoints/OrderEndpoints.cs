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

			groupBuilder.MapGet("/orders/{orderId}/aprove", async (Guid orderId, AproveOrder aproveOrder) =>
			{
				await aproveOrder.Execute(orderId);
				return Results.Ok();
			}).WithOpenApi();

			return app;
		}
	}
}