namespace PizzaShop.API.UseCases
{
	public abstract class UseCaseBase<T>
	{
		public abstract Task Execute(T data);
	}
}