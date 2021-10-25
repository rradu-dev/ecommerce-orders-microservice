using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Application.Commands;
using Ecommerce.Services.Orders.Application.Dtos;
using Ecommerce.Services.Orders.Application.Repositories;

namespace Ecommerce.Services.Orders.Application.Handlers
{
    internal class CreateOrderHandler
		: IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IMapper _mapper;

        public CreateOrderHandler(IRepository<Order, Guid> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Order>(request);

			entity.Total = entity.Items.Aggregate<OrderItem, double>(0.0d,
				(total, item) => total += item.UnitPrice * item.Quantity);

			entity = await _repository.AddAsync(entity);
            return _mapper.Map<OrderDto>(entity);
        }
    }
}
