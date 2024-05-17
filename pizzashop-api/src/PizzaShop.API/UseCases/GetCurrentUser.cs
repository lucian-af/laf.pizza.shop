using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class GetCurrentUser(
		IAuthenticate authenticate,
		IHttpContextAccessor httpContextAccessor) : UseCaseBase<UserTokenDto>
	{
		private readonly IAuthenticate _authenticate = authenticate;
		private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

		public override Task<Result<UserTokenDto>> Execute()
		{
			var token = _httpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;
			ArgumentException.ThrowIfNullOrWhiteSpace(token, nameof(token));

			var payload = _authenticate.GetPayload<UserTokenDto>(token)
				?? throw new ArgumentException("Unauthorized.");

			return Task.FromResult(new Result<UserTokenDto> { Data = payload });
		}
	}
}