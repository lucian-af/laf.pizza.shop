using PizzaShop.API.Domain.Entities;

namespace PizzaShop.API.UseCases
{
	public class SignOut(IHttpContextAccessor httpContextAccessor) : UseCaseBase
	{
		private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

		public override Task Execute()
		{
			_httpContext.Response.Cookies.Delete(AuthCookies.AuthCookieName);
			return Task.CompletedTask;
		}
	}
}