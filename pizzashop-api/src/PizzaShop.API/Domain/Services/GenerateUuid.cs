using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.Domain.Services
{
	public sealed class GenerateUuid : IGenerateCode
	{
		public string GenerateCode()
			=> Guid.NewGuid().ToString().Replace("-", "");
	}
}