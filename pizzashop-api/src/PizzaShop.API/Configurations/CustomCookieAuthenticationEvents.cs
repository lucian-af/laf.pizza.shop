using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using PizzaShop.API.Authentication;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Configurations
{
	public class CustomCookieAuthenticationEvents(IAuthenticate authenticate, IOptions<PizzaShopConfigs> configs) : CookieAuthenticationEvents
	{
		private readonly IAuthenticate _authenticate = authenticate;
		private readonly string UrlLogin = $"{configs.Value.AppUrl}/{configs.Value.AppUrlLogin}";

		public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
		{
			var userPrincipal = context.Principal;
			var token = (from c in userPrincipal.Claims
						 where c.Type == "token"
						 select c.Value).FirstOrDefault();

			if (!_authenticate.Valid(token))
			{
				context.RejectPrincipal();
				await context.HttpContext.SignOutAsync(
					CookieAuthenticationDefaults.AuthenticationScheme);
			}
		}

		public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			return Task.CompletedTask;
		}

		public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			return Task.CompletedTask;
		}
	}
}