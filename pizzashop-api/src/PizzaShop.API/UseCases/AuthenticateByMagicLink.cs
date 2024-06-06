using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public sealed class AuthenticateByMagicLink(
		IUserRepository _userRepository,
		IRestaurantRepository _restaurantRepository,
		IAuthenticate _authenticate,
		IHttpContextAccessor httpContextAccessor) : UseCaseBase<string>
	{
		private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

		public override Task Execute(string code)
		{
			var authLinkFromCode = _userRepository.GetAuthLinkFromCode(code)
				?? throw new NullValueException("Link not found;");

			if (!authLinkFromCode.IsAuthLinkValid())
				throw new DomainException("Auth link expired.");

			var restaurantManager = _restaurantRepository.GetResturantFromManager(authLinkFromCode.UserId);

			var payloadToken = new UserTokenDto
			{
				RestaurantId = restaurantManager?.Id.ToString(),
				UserId = authLinkFromCode.UserId.ToString()
			};
			var token = _authenticate.Generate(authLinkFromCode.AuthLinkExpiration, payloadToken);

			_userRepository.DeleteAuthLinkFromCode(code);
			_userRepository.UnitOfWork.Commit();

			SetCookies(token, authLinkFromCode.AuthLinkExpiration, restaurantManager?.Manager.Role ?? RoleUser.Customer);

			return Task.CompletedTask;
		}

		// TODO: refactor
		private void SetCookies(string token, DateTime expireIn, RoleUser role)
		{
			var claims = new Claim[]
			{
				new("token", token),
				new(ClaimTypes.Role, role.ToString().ToLower())
			};
			_httpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
				new AuthenticationProperties { IsPersistent = true, ExpiresUtc = expireIn });
		}
	}
}