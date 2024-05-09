using Microsoft.Extensions.Options;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;
using PizzaShop.API.Settings;
using PizzaShop.API.Smtp.Adapters;
using PizzaShop.API.Smtp.CommonMessages;

namespace PizzaShop.API.UseCases
{
	public class AuthenticateUser(
		IAuthLinkRepository authLinkRepository,
		IGenerateCode generateCode,
		IOptions<PizzaShopConfigs> pizzaShopConfigs,
		IMailAdapter mailAdapter)
	{
		private readonly IAuthLinkRepository _authLinkRepository = authLinkRepository;
		private readonly IGenerateCode _generateCode = generateCode;
		private readonly PizzaShopConfigs _pizzaShopConfigs = pizzaShopConfigs.Value;
		private readonly IMailAdapter _mailAdapter = mailAdapter;

		public async Task Execute(AuthenticateDto data)
		{
			ArgumentNullException.ThrowIfNull(data, nameof(data));
			ArgumentException.ThrowIfNullOrWhiteSpace(data.Email, nameof(data.Email));

			var userFromEmail = _authLinkRepository.GetUserFromEmail(data.Email)
				?? throw new NotFoundException("User not found.");

			var authLinkCode = _generateCode.GenerateCode();

			_authLinkRepository.AddAuthLink(new(authLinkCode, userFromEmail.Id));
			var success = await _authLinkRepository.UnitOfWork.Commit();

			if (!success)
				throw new Exception("Error on generate authentication.");

			var authLink = $"{_pizzaShopConfigs.BaseUrl}/authenticate/auth-links?code={authLinkCode}&redirect={_pizzaShopConfigs.AuthRedirectUrl}";

			// TODO: refactor
			_mailAdapter.SendMail(new EmailMessage
			{
				Title = $"Hello, {userFromEmail.Name}, authenticate to Pizza Shop",
				To = data.Email,
				Body = $"Use the following link to authenticate on Pizza Shop: {authLink}"
			});
		}
	}
}