namespace PizzaShop.API.UseCases
{
	public abstract class UseCaseBase<I>
	{
		public abstract Task Execute(I data);
	}

	public abstract class UseCaseBase
	{
		public abstract Task Execute();
	}

	public class Result<T>
	{
		public T Data { get; set; }
	}
}