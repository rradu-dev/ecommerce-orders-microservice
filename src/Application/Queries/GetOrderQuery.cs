using System;
using MediatR;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Queries
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public Guid Id { get; set; }

        public GetOrderQuery(Guid id)
        {
            Id = id;
        }
    }
}
