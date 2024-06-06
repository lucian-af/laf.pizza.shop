using PizzaShop.API.Authentication;
using PizzaShop.API.Domain.Models;

namespace PizzaShop.API.UseCases
{
	public abstract class UseCaseBase<I>(IAuthenticate _authenticate = null, bool _resturantRequired = false) : IUseCaseBase
	{
		protected UserTokenDto UserToken => GetPayload();

		public virtual Task Execute(I data) => Task.CompletedTask;

		public virtual Task<Result<I>> Execute() => Task.FromResult(new Result<I>());

		public virtual Task<Result<T>> Execute<T>(I data) => Task.FromResult(new Result<T>());

		Task IUseCaseBase.Execute() => Task.CompletedTask;

		private UserTokenDto GetPayload()
		{
			if (_authenticate is null)
				return null;

			var payload = _authenticate.GetPayload<UserTokenDto>();

			return payload is null || (_resturantRequired && payload.RestaurantId is null)
				? throw new ArgumentException("Unauthorized.")
				: payload;
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