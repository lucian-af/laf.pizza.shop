using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class UserEndpoints
	{
		public static IEndpointRouteBuilder UseUserEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapGet("/user/me", async (GetCurrentUser getCurrentUser) =>
			{
				var result = await getCurrentUser.Execute();
				return Results.Ok(result.Data);
			}).WithOpenApi();

			return app;
		}
	}
}