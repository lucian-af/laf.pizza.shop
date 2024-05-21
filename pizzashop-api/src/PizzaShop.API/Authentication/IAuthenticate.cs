namespace PizzaShop.API.Authentication
{
	public interface IAuthenticate
	{
		string Generate<T>(DateTime expireIn, T payload);

		bool Valid(string token);

		T GetPayload<T>();
	}
}