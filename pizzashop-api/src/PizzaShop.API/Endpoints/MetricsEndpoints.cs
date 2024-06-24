using PizzaShop.API.UseCases;

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

			groupBuilder.MapGet("/metrics/day-orders-amount", async (GetDayOrdersAmount getDayOrdersAmount) =>
			{
				var result = await getDayOrdersAmount.Execute();
				return Results.Ok(result.Data);
			}).WithOpenApi();

			groupBuilder.MapGet("/metrics/month-orders-amount", async (GetMonthOrdersAmount getMonthOrdersAmount) =>
			{
				var result = await getMonthOrdersAmount.Execute();
				return Results.Ok(result.Data);
			}).WithOpenApi();

			groupBuilder.MapGet("/metrics/month-canceled-orders-amount", async (GetMonthCanceledOrdersAmount getMonthCanceledOrdersAmount) =>
			{
				var result = await getMonthCanceledOrdersAmount.Execute();
				return Results.Ok(result.Data);
			}).WithOpenApi();

			groupBuilder.MapGet("/metrics/popular-products", async (GetPopularProducts getPopularProducts) =>
			{
				var result = await getPopularProducts.Execute();
				return Results.Ok(result.Data);
			}).WithOpenApi();

			groupBuilder.MapGet("/metrics/daily-revenue-in-period", async (DateTime from, DateTime to, GetDailyRevenueInPeriod getDailyRevenueInPeriod) =>
			{
				var result = await getDailyRevenueInPeriod.Execute(new() { From = from, To = to });
				return Results.Ok(result.Data);
			}).WithOpenApi();

			return app;
		}
	}
}