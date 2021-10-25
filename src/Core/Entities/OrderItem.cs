using System;

namespace Ecommerce.Services.Orders.Core.Entities
{
    public class OrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
