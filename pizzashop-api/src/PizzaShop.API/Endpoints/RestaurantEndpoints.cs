﻿using PizzaShop.API.Domain.Models;
using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class RestaurantEndpoints
	{
		public static IEndpointRouteBuilder UseRestaurantEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapPost("restaurant", async (AddRestaurantDto request, AddRestaurant service) =>
			{
				await service.Execute(request);
				return Results.Created();
			}).WithOpenApi();

			groupBuilder.MapGet("restaurant/managed-restaurant", async (GetManagedRestaurant getManagedRestaurant) =>
			{
				var result = await getManagedRestaurant.Execute();
				return Results.Ok(result.Data);
			}).WithOpenApi();

			return app;
		}
	}
}