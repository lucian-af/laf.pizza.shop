using PizzaShop.API.Domain.Models;
using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class AuthenticationEndpoints
	{
		public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapPost("/authenticate", async (AuthenticateDto authenticateDto, AuthenticateUser authenticateUser) =>
			{
				await authenticateUser.Execute(authenticateDto);
				return Results.Created();
			}).WithOpenApi();

			return app;
		}
	}
}