namespace PizzaShop.API.Infrastructure.Data
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}