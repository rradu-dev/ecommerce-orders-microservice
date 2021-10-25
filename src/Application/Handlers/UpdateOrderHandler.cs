using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Exceptions;
using Ecommerce.Services.Orders.Application.Commands;
using Ecommerce.Services.Orders.Application.Repositories;

namespace Ecommerce.Services.Orders.Application.Handlers
{
    internal class UpdateOrderHandler : AsyncRequestHandler<UpdateOrderCommand>
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IRepository<Order, Guid> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateOrderCommand request,
            CancellationToken cancellationToken)
        {
			if (await _repository.ExistsAsync(e => e.Id == request.Id))
			{
				var order = _mapper.Map<Order>(request);
				var entity = await _repository.GetAsync(order.Id);

				entity.CartId = order.CartId;
				entity.Status = order.Status;
				entity.Customer = order.Customer;
				entity.ShippingAddress = order.ShippingAddress;
				entity.Items = order.Items;
				entity.Total = order.Items.Aggregate<OrderItem, double>(0.0d,
					(total, item) => total += item.UnitPrice * item.Quantity);

				await _repository.UpdateAsync(entity);
				return;
			}

			throw new NotFoundException(request.Id);
        }
    }
}
