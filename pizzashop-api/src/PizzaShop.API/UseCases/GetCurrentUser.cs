using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class GetCurrentUser(IAuthenticate authenticate) : UseCaseBase<UserTokenDto>
	{
		private readonly IAuthenticate _authenticate = authenticate;

		public override Task<Result<UserTokenDto>> Execute()
		{
			var payload = _authenticate.GetPayload<UserTokenDto>()
				?? throw new ArgumentException("Unauthorized.");

			return Task.FromResult(new Result<UserTokenDto> { Data = payload });
		}
	}
}