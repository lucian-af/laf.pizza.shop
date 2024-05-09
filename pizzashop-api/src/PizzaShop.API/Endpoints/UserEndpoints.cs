namespace PizzaShop.API.Endpoints
{
	public static class UserEndpoints
	{
		public static IEndpointRouteBuilder UseUserEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapGet("user/{userId}", (Guid userId) =>
			{
				return Results.Ok(userId);
			}).WithOpenApi();

			return app;
		}
	}
}