using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PizzaShop.API.UseCases
{
	public class SignOut(IHttpContextAccessor httpContextAccessor) : UseCaseBase
	{
		private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

		public override Task Execute()
			=> _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	}
}