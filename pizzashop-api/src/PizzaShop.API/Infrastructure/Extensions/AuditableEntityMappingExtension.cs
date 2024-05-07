using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaShop.API.Domain.Entities;

namespace PizzaShop.API.Infrastructure.Extensions
{
	public static class AuditableMappingExtension
	{
		public static void AddMappingForAuditableProperties<T>(this EntityTypeBuilder<T> builder) where T : Auditable
		{
			builder.Property(a => a.CreatedAt)
				.HasColumnType("timestamp")
				.IsRequired()
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.ValueGeneratedOnAdd()
				.HasColumnName("createdAt");

			builder.Property(a => a.UpdatedAt)
				.HasColumnType("timestamp")
				.IsRequired(false)
				.HasColumnName("updatedAt")
				.ValueGeneratedNever();
		}
	}
}
