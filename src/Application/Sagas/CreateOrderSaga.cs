using System;
using System.Threading.Tasks;
using Chronicle;
using Ecommerce.Services.Orders.Application.Events;
using Ecommerce.Services.Orders.Application.Repositories;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Application.Sagas
{
    public class CreateOrderSaga : Saga<CreateOrderSagaData>,
		ISagaStartAction<OrderCreatedEvent>,
		ISagaAction<OrderStockReservedEvent>,
		ISagaAction<OrderStockRejectedEvent>
    {
		private readonly IRepository<Order, Guid> _repository;

        public CreateOrderSaga(IRepository<Order, Guid> repository)
        {
			_repository = repository;
        }

        public Task HandleAsync(OrderCreatedEvent message, ISagaContext context)
        {
            Data.CustomerId = message.CustomerId;
            Data.OrderId = message.OrderId;
			Data.OrderStatus = OrderStatus.Created;

			// await _busPublisher.PublishAsync("topic", new StockReserveOrderEvent(message.OrderId));

			return Task.CompletedTask;
        }

        public Task CompensateAsync(OrderCreatedEvent message, ISagaContext context)
        {
            return RejectAsync();
        }

        public Task HandleAsync(OrderStockReservedEvent message, ISagaContext context)
        {
			throw new NotImplementedException();
        }

        public Task CompensateAsync(OrderStockReservedEvent message, ISagaContext context)
        {
            return RejectAsync();
        }

        public async Task HandleAsync(OrderStockRejectedEvent message, ISagaContext context)
        {
			var order = await _repository.GetAsync(Data.OrderId);
			order.Status = OrderStatus.Cancelled;
			await _repository.UpdateAsync(order);
        }

        public Task CompensateAsync(OrderStockRejectedEvent message, ISagaContext context)
        {
            return RejectAsync();
        }
    }
}
