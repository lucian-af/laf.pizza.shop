﻿using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class MetricsEndpoints
	{
		public static IEndpointRouteBuilder UseMetricsEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapGet("/metrics/month-revenue", async (GetMonthRevenue getMonthRevenue) =>
			{
				var result = await getMonthRevenue.Execute();
				return Results.Ok(result.Data);
			}).WithOpenApi();

			return app;
		}
	}
}