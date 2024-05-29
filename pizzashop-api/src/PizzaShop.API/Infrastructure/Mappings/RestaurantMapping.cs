using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Infrastructure.Extensions;

namespace PizzaShop.API.Infrastructure.Mappings
{
	public class RestaurantMapping : IEntityTypeConfiguration<Restaurant>
	{
		public void Configure(EntityTypeBuilder<Restaurant> builder)
		{
			builder.AddMappingForAuditableProperties();

			builder.ToTable("restaurants")
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.HasColumnName("id");

			builder
				.Property(u => u.Name)
				.HasColumnName("name")
				.HasColumnType("text")
				.IsRequired();

			builder
				.Property(u => u.Description)
				.HasColumnName("description")
				.HasColumnType("text")
				.IsRequired(false);

			builder
				.Property(u => u.ManagerId)
				.HasColumnName("managerId");

			builder.HasOne(r => r.Manager)
				.WithMany(u => u.Restaurants)
				.HasForeignKey(r => r.ManagerId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull)
				.HasConstraintName("restaurant_manager");
		}
	}
}