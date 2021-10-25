using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Pagination;
using Ecommerce.Services.Orders.Application.Queries;
using Ecommerce.Services.Orders.Application.Dtos;
using Ecommerce.Services.Orders.Application.Repositories;

namespace Ecommerce.Services.Orders.Application.Handlers
{
    internal class GetOrdersHandler
		: IRequestHandler<GetOrdersQuery, PageList<OrderDto>>
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IMapper _mapper;

        public GetOrdersHandler(IRepository<Order, Guid> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageList<OrderDto>> Handle(GetOrdersQuery request,
            CancellationToken cancellationToken)
        {
			var entities = await _repository.GetPageAsync(
				request.Page + 1, request.Size);
			return _mapper.Map<PageList<OrderDto>>(entities);
        }
    }
}
