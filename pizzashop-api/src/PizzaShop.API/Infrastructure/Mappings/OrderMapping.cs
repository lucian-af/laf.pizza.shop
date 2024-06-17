using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Infrastructure.Extensions;

namespace PizzaShop.API.Infrastructure.Mappings
{
	public class OrderMapping : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.AddMappingForAuditableProperties();

			builder.ToTable("orders",
				o => o.HasCheckConstraint("CK_Order_Status", "status in (0,1,2,3,4)"))
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.HasColumnName("id");

			builder
				.Property(u => u.Status)
				.HasColumnName("status")
				.HasColumnType("smallint")
				.IsRequired()
				.HasDefaultValue(OrderStatus.Pending);

			builder
				.Property(u => u.Total)
				.HasColumnName("total")
				.HasColumnType("money")
				.IsRequired();

			builder
				.Property(u => u.CustomerId)
				.HasColumnName("customerId")
				.IsRequired(false);

			builder
				.Property(u => u.RestaurantId)
				.HasColumnName("restaurantId")
				.IsRequired();

			builder.HasOne(r => r.Customer)
				.WithMany(u => u.Orders)
				.HasForeignKey(r => r.CustomerId)
				.OnDelete(DeleteBehavior.SetNull)
				.HasConstraintName("order_customer");

			builder.HasOne(r => r.Restaurant)
				.WithMany(u => u.Orders)
				.HasForeignKey(r => r.RestaurantId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("order_resturant");
		}
	}
}