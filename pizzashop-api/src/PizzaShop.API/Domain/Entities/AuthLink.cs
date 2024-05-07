using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.Domain.Entities
{
	public sealed class AuthLink : Auditable, IAggregateRoot
	{
		public string Code { get; }
		public Guid UserId { get; }

		public User User { get; }

		public AuthLink(string code, Guid userId)
		{
			if (string.IsNullOrWhiteSpace(code))
				throw new DomainException("'code' is required!");

			if (userId == Guid.Empty)
				throw new DomainException("'userId' is required!");

			Code = code;
			UserId = userId;
		}
	}
}