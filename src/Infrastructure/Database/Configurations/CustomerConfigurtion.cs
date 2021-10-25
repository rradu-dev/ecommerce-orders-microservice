using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.ToTable("customer");

			builder.HasKey(e => e.Id);
			builder.Property<Guid>(e => e.CustomerId)
				.IsRequired();
			builder.Property<string>(e => e.FirstName)
				.HasMaxLength(24)
				.IsRequired();
			builder.Property<string>(e => e.LastName)
				.HasMaxLength(24)
				.IsRequired();
			builder.Property<string>(e => e.Email)
				.HasMaxLength(64)
				.IsRequired();
			builder.Property<string>(e => e.PhoneNumber)
				.HasMaxLength(12)
				.IsRequired();

			builder.HasOne(e => e.Order)
				.WithOne(e => e.Customer);
		}
	}
}
