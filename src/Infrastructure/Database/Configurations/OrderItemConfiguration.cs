using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.HasKey(i => i.Id);
            builder.Property<Guid>(i => i.ProductId)
                .IsRequired();
            builder.Property<string>(i => i.Title)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property<string>(i => i.Description)
                .HasMaxLength(255);
            builder.Property<string>(i => i.Thumbnail)
                .HasMaxLength(128);
            builder.Property<int>(i => i.Quantity)
                .IsRequired();
            builder.Property<double>(i => i.UnitPrice)
                .IsRequired();

            builder.HasOne(i => i.Order)
                .WithMany(o => o.Items);
        }
    }
}
