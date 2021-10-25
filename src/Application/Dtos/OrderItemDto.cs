using System;

namespace Ecommerce.Services.Orders.Application.Dtos
{
    public class OrderItemDto
    {
		public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
