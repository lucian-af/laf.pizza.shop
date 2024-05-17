namespace PizzaShop.API.UseCases
{
	public abstract class UseCaseBase<I>
	{
		public virtual Task Execute(I data) => Task.CompletedTask;

		public virtual Task<Result<I>> Execute() => Task.FromResult(new Result<I>());
	}

	public abstract class UseCaseBase
	{
		public virtual Task Execute() => Task.CompletedTask;
	}

	public class Result<T>
	{
		public T Data { get; set; }
	}
}