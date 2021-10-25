using System;
using System.Collections.Generic;

namespace Ecommerce.Services.Orders.Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
		public string Status { get; set; }
        public CustomerDto Customer { get; set; }
		public AddressDto ShippingAddress { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
		public double Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
