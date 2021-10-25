using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Exceptions;
using Ecommerce.Services.Orders.Application.Queries;
using Ecommerce.Services.Orders.Application.Dtos;
using Ecommerce.Services.Orders.Application.Repositories;

namespace Ecommerce.Services.Orders.Application.Handlers
{
    internal class GetOrderHandler
		: IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IMapper _mapper;

        public GetOrderHandler(IRepository<Order, Guid> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request,
            CancellationToken cancellationToken)
        {
			if (await _repository.ExistsAsync(e => e.Id == request.Id))
			{
				var entities = await _repository.GetAsync(request.Id);
				return _mapper.Map<OrderDto>(entities);
			}

			throw new NotFoundException(request.Id);
        }
    }
}
