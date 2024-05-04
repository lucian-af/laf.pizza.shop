using PizzaShop.API.Infrastructure.Data;

namespace PizzaShop.API.Domain.Interfaces
{
	public interface IRepository<T> : IDisposable where T : Entity
	{
		IUnitOfWork UnitOfWork { get; }
	}
}