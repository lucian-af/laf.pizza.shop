using Microsoft.AspNetCore.Authorization;
using PizzaShop.API.Domain.Models;
using PizzaShop.API.UseCases;

namespace PizzaShop.API.Endpoints
{
	public static class AuthenticationEndpoints
	{
		public static IEndpointRouteBuilder UseAuthenticationEndpoints(this IEndpointRouteBuilder app)
		{
			var groupBuilder = MainEndpoints.GetRouteGroupBuilder(app);

			groupBuilder.MapPost("/authenticate", [AllowAnonymous] async (AuthenticateDto authenticateDto, AuthenticateUser authenticateUser) =>
			{
				await authenticateUser.Execute(authenticateDto);
				return Results.Created();
			}).WithOpenApi();

			// TODO: refactor
			//groupBuilder.MapGet("/authenticate/auth-links", [AllowAnonymous] async (AuthenticateDto authenticateDto, AuthenticateUser authenticateUser) =>
			//{
			//	await authenticateUser.Execute(authenticateDto);
			//	return Results.Created();
			//}).WithOpenApi();

			return app;
		}
	}
}