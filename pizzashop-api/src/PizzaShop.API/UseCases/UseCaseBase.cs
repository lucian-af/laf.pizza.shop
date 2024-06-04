using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public abstract class UseCaseBase<I>(IAuthenticate _authenticate = null) : IUseCaseBase
	{
		protected UserTokenDto UserToken => GetPayload();

		public virtual Task Execute(I data) => Task.CompletedTask;

		public virtual Task<Result<I>> Execute() => Task.FromResult(new Result<I>());

		Task IUseCaseBase.Execute() => Task.CompletedTask;

		private UserTokenDto GetPayload()
		{
			if (_authenticate is null)
				return null;

			return _authenticate.GetPayload<UserTokenDto>() ?? throw new ArgumentException("Unauthorized.");
		}
	}

	public interface IUseCaseBase
	{
		public Task Execute();
	}

	public class Result<T>
	{
		public T Data { get; set; }
	}
}