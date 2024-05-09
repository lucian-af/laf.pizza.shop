namespace PizzaShop.API.Authentication
{
	public interface IAuthenticate
	{
		string Generate(DateTime expireIn, Dictionary<string, string> payload = null);

		bool Valid(string token);
	}
}