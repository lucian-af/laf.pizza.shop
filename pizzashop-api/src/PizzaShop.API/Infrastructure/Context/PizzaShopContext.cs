using Microsoft.EntityFrameworkCore;
using Npgsql;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Entities.Authenticate;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Infrastructure.Data;

namespace PizzaShop.API.Infrastructure.Context
{
	public class PizzaShopContext(DbContextOptions options) : DbContext(options), IUnitOfWork
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<AuthLink> AuthLinks { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PizzaShopContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}

		public async Task<bool> Commit()
		{
			foreach (var entry in ChangeTracker.Entries<Auditable>().ToList())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedAt = DateTime.Now;
						break;

					case EntityState.Modified:
						entry.Entity.UpdatedAt = DateTime.Now;
						break;
				}
			}

			return await base.SaveChangesAsync() > 0;
		}

		public class Teste : INpgsqlNameTranslator
		{
			public string TranslateMemberName(string clrName) => throw new NotImplementedException();

			public string TranslateTypeName(string clrName) => throw new NotImplementedException();
		}
	}
}