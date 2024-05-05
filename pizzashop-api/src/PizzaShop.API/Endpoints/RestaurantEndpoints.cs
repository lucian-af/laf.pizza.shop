using PizzaShop.API.Domain.Models;
using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class RestaurantEndpoints
	{
		public static IEndpointRouteBuilder MapRestaurantEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapPost("restaurant", async (AddRestaurantDto request, AddRestaurant service) =>
			{
				await service.Execute(request);
				return Results.Created();
			}).WithOpenApi();

			return app;
		}
	}
}