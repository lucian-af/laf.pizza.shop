using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Models;
using PizzaShop.API.Settings;
using PizzaShop.API.Smtp.Adapters;
using PizzaShop.API.Smtp.CommonMessages;

namespace PizzaShop.API.UseCases
{
	public class GenerateMagicLink(
		IUserRepository userRepository,
		IGenerateCode generateCode,
		IOptions<PizzaShopConfigs> pizzaShopConfigs,
		IMailAdapter mailAdapter) : UseCaseBase<GenerateMagicLinkDto>
	{
		private readonly IUserRepository _userRepository = userRepository;
		private readonly IGenerateCode _generateCode = generateCode;
		private readonly PizzaShopConfigs _pizzaShopConfigs = pizzaShopConfigs.Value;
		private readonly IMailAdapter _mailAdapter = mailAdapter;

		public override async Task Execute(GenerateMagicLinkDto data)
		{
			ArgumentNullException.ThrowIfNull(data, nameof(data));
			ArgumentException.ThrowIfNullOrWhiteSpace(data.Email, nameof(data.Email));

			// TODO: refactor: armazenar link no banco, recuperar para o caso de reenviar
			var userFromEmail = _userRepository.GetUserFromEmail(data.Email)
				?? throw new NullValueException("User not found.");

			var authLinkCode = _generateCode.GenerateCode();

			_userRepository.AddAuthLink(new(authLinkCode, userFromEmail.Id));
			var success = await _userRepository.UnitOfWork.Commit();

			if (!success)
				throw new Exception("Error on generate authentication.");

			var urlBase = $"{_pizzaShopConfigs.BaseUrl}/api/v1/authenticate/auth-links";
			var queryParams = new Dictionary<string, string>
			{
				{ "code", authLinkCode },
				{ "redirect", _pizzaShopConfigs.AppUrl }
			};
			var authLink = new Uri(QueryHelpers.AddQueryString(urlBase, queryParams));

			// TODO: refactor: usar Event Pattern
			_mailAdapter.SendMail(new EmailMessage
			{
				Title = $"Hello {userFromEmail.Name}, authenticate to Pizza Shop",
				To = data.Email,
				Body = $"Use the following link to authenticate on Pizza Shop: {authLink}"
			});
		}
	}
}