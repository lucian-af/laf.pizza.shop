using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PizzaShop.API.Domain.Models;
using PizzaShop.API.Settings;
using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class AuthenticationEndpoints
	{
		public static IEndpointRouteBuilder UseAuthenticationEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapPost("/authenticate", [AllowAnonymous] async
				(GenerateMagicLinkDto data, GenerateMagicLink generateMagicLink) =>
			{
				await generateMagicLink.Execute(data);
				return Results.Created();
			}).WithOpenApi();

			groupBuilder.MapGet("/authenticate/auth-links", [AllowAnonymous] async (
				[FromQuery] string code,
				[FromQuery] string redirect,
				AuthenticateByMagicLink authenticateByMagicLink) =>
			{
				await authenticateByMagicLink.Execute(code);
				return Results.Redirect(redirect);
			}).WithOpenApi();

			groupBuilder.MapGet("/authenticate/sign-out", async (SignOut signOut, IOptions<PizzaShopConfigs> pizzaShopConfigs) =>
			{
				await signOut.Execute();
				return Results.Redirect($"{pizzaShopConfigs.Value.AppUrl}/{pizzaShopConfigs.Value.AppUrlLogin}");
			}).WithOpenApi();

			return app;
		}
	}
}