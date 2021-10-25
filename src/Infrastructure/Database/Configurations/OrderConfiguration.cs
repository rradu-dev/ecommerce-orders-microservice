using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("order");

			builder.HasKey(e => e.Id);
			builder.Property<Guid>(e => e.CartId)
				.IsRequired();
			builder.Property<OrderStatus>(e => e.Status)
				.HasMaxLength(16)
				.HasConversion(
					v => v.ToString(),
					v => (OrderStatus) Enum.Parse(typeof(OrderStatus), v));
			builder.Property<DateTime>(e => e.CreatedAt)
                .ValueGeneratedOnAdd()
				.HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property<DateTime>(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			builder.HasOne(e => e.Customer)
				.WithOne(e => e.Order)
				.HasForeignKey<Customer>(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasOne(e => e.ShippingAddress)
				.WithOne(e => e.Order)
				.HasForeignKey<Address>(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Items)
                .WithOne(e => e.Order)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

			builder.HasIndex(e => e.CartId)
				.IsUnique();

			builder.Navigation(e => e.Customer)
				.AutoInclude();
			builder.Navigation(e => e.ShippingAddress)
				.AutoInclude();
			builder.Navigation(e => e.Items)
				.AutoInclude();
		}
	}
}
