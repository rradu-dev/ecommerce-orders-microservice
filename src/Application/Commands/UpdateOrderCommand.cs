using System;
using System.Collections.Generic;
using MediatR;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid Id { get; set; }
		public Guid CartId { get; set; }
		public OrderStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
		public AddressDto ShippingAddress { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
