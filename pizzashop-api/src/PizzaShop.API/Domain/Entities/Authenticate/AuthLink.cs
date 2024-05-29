using PizzaShop.API.Domain.Exceptions;
using PizzaShop.API.Domain.Interfaces;

namespace PizzaShop.API.Domain.Entities.Authenticate
{
	public sealed class AuthLink : Auditable, IAggregateRoot
	{
		public DateTime AuthLinkExpiration { get; } = DateTime.Now.AddDays(AuthCookies.TotalDaysAuthLinkExpiration);

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

		public bool IsAuthLinkValid()
		{
			var daysSinceAuthLinkWasCreated = DateTime.Now.Day - CreatedAt.Day;
			if (daysSinceAuthLinkWasCreated > AuthCookies.TotalDaysAuthLinkExpiration)
				return false;

			return true;
		}
	}
}