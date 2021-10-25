using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Exceptions;
using Ecommerce.Services.Orders.Application.Commands;
using Ecommerce.Services.Orders.Application.Repositories;

namespace Ecommerce.Services.Orders.Application.Handlers
{
    internal class DeleteOrderHandler
		: AsyncRequestHandler<DeleteOrderCommand>
    {
        private readonly IRepository<Order, Guid> _repository;

        public DeleteOrderHandler(IRepository<Order, Guid> repository)
        {
            _repository = repository;
        }

        protected override async Task Handle(DeleteOrderCommand request,
            CancellationToken cancellationToken)
        {
			if (await _repository.ExistsAsync(e => e.Id == request.Id))
			{
				await _repository.DeleteAsync(request.Id);
				return;
			}

			throw new NotFoundException(request.Id);
        }
    }
}
