using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Configurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("address");

			builder.HasKey(e => e.Id);
			builder.Property<string>(e => e.Number)
				.HasMaxLength(6)
				.IsRequired();
			builder.Property<string>(e => e.Street)
				.HasMaxLength(100)
				.IsRequired();
			builder.Property<string>(e => e.State)
				.HasMaxLength(24)
				.IsRequired();
			builder.Property<string>(e => e.City)
				.HasMaxLength(24)
				.IsRequired();
			builder.Property<string>(e => e.Country)
				.HasMaxLength(24)
				.IsRequired();
			builder.Property<string>(e => e.PostalCode)
				.HasMaxLength(6)
				.IsRequired();

			builder.HasOne(e => e.Order)
				.WithOne(e => e.ShippingAddress);
		}
	}
}
