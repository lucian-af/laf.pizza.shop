namespace PizzaShop.API.Domain.Exceptions
{
	public class UnauthorizedException : Exception
	{
		public UnauthorizedException(string message) : base(message)
		{ }

		public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
		{ }

		public static void ThrowIfNullOrWhiteSpace(string argument, string? message = "Unauthorized")
		{
			if (string.IsNullOrWhiteSpace(argument))
				throw new UnauthorizedException(message);
		}
	}
}