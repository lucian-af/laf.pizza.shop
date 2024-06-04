using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Extensions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public class GetCurrentUser(IAuthenticate authenticate, IUserRepository _userRepository)
		: UseCaseBase<GetUserDto>(authenticate)
	{
		public override Task<Result<GetUserDto>> Execute()
		{
			ArgumentNullException.ThrowIfNull(UserToken, nameof(UserToken));

			var user = _userRepository.GetUserById(UserToken.UserId.ToGuid())
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