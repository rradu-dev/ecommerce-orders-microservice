using System;
using MediatR;
using Ecommerce.Services.Orders.Core.Pagination;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Queries
{
    public class GetOrdersByCustomerIdQuery : IRequest<PageList<OrderDto>>
    {
		public Guid CustomerId { get; set; }
        public int Page { get; set; }
		public int Size { get; set; }

        public GetOrdersByCustomerIdQuery(Guid customerId, int page, int size)
        {
			CustomerId = customerId;
            Page = page;
			Size = size;
        }
    }
}
