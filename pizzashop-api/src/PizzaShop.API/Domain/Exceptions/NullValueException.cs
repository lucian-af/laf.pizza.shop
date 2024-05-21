namespace PizzaShop.API.Domain.Exceptions
{
	public class NullValueException : Exception
	{
		public NullValueException(string message) : base(message)
		{ }

		public NullValueException(string message, Exception innerException) : base(message, innerException)
		{ }
	}
}