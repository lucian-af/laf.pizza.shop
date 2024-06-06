using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PizzaShop.API.UseCases
{
	public sealed class SignOut(IHttpContextAccessor httpContextAccessor) : IUseCaseBase
	{
		private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

		public Task Execute()
			=> _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	}
}