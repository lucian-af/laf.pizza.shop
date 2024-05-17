using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class AuthenticateByMagicLink(
		IAuthLinkRepository authLinkRepository,
		IRestaurantRepository restaurantRepository,
		IAuthenticate authenticate,
		IHttpContextAccessor httpContextAccessor) : UseCaseBase<string>
	{
		private readonly IAuthLinkRepository _authLinkRepository = authLinkRepository;
		private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
		private readonly IAuthenticate _authenticate = authenticate;
		private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

		public override Task Execute(string code)
		{
			var authLinkFromCode = _authLinkRepository.GetAuthLinkFromCode(code)
				?? throw new NotFoundException("Link not found;");

			if (!authLinkFromCode.IsAuthLinkValid())
				throw new DomainException("Auth link expired.");

			var restaurantManager = _restaurantRepository.GetResturantFromManager(authLinkFromCode.UserId);

			var payloadToken = new UserTokenDto
			{
				RestaurantId = restaurantManager?.Id.ToString(),
				UserId = authLinkFromCode.UserId.ToString()
			};
			var token = _authenticate.Generate(authLinkFromCode.AuthLinkExpiration, payloadToken);

			_authLinkRepository.DeleteAuthLinkFromCode(code);
			_authLinkRepository.UnitOfWork.Commit();

			SetCookies(token, authLinkFromCode.AuthLinkExpiration);

			return Task.CompletedTask;
		}

		// TODO: refactor
		private void SetCookies(string token, DateTime expireIn)
		{
			var claims = new Claim[] { new("token", token) };
			_httpContext.SignInAsync(
				  CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
				new AuthenticationProperties { IsPersistent = true, ExpiresUtc = expireIn });
		}
	}
}