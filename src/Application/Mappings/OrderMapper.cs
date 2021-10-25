using AutoMapper;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Pagination;
using Ecommerce.Services.Orders.Application.Commands;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Mappings
{
    internal class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<UpdateOrderCommand, Order>();
            CreateMap<Order, OrderDto>();
			CreateMap<PageList<Order>, PageList<OrderDto>>();
        }
    }
}
