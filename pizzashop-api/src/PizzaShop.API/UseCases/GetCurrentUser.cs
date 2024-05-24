using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class GetCurrentUser(
		IAuthenticate authenticate,
		IUserRepository userRepository) : UseCaseBase<GetUserDto>
	{
		private readonly IAuthenticate _authenticate = authenticate;
		private readonly IUserRepository _userRepository = userRepository;

		public override Task<Result<GetUserDto>> Execute()
		{
			var payload = _authenticate.GetPayload<UserTokenDto>()
				?? throw new ArgumentException("Unauthorized.");

			ArgumentNullException.ThrowIfNull(payload, nameof(payload));

			var user = _userRepository.GetUserById(payload.UserId.ToGuid())
				?? throw new NullValueException("User not found");

			var currentUser = new GetUserDto
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				Phone = user.Phone,
				Role = user.Role,
			};

			return Task.FromResult(new Result<GetUserDto> { Data = currentUser });
		}
	}
}