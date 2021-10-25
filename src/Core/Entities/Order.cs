using System;
using System.Collections.Generic;

namespace Ecommerce.Services.Orders.Core.Entities
{
    public class Order : IEntity<Guid>, IAuditableEntity
    {
        public Guid Id { get; set; }
		public Guid CartId { get; set; }
		public OrderStatus Status { get; set; }
        public Customer Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
		public double Total { get; set; }
    }
}
