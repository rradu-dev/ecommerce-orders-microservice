using AutoMapper;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Mappings
{
    internal class AddressMapper : Profile
    {
        public AddressMapper()
        {
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();
        }
    }
}
