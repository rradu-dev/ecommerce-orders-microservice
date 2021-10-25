using AutoMapper;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Mappings
{
    internal class OrderItemMapper : Profile
    {
        public OrderItemMapper()
        {
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
