using AutoMapper;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Mappings
{
    internal class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>();
        }
    }
}
