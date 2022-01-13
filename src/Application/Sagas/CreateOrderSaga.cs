using System;
using System.Threading.Tasks;
using Chronicle;
using Ecommerce.Services.Orders.Application.Events;
using Ecommerce.Services.Orders.Application.Repositories;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Application.Sagas
{
    public class CreateOrderSaga : Saga<CreateOrderSagaData>,
		ISagaStartAction<OrderApprovedEvent>,
		ISagaAction<OrderStockReservedEvent>,
		ISagaAction<OrderStockRejectedEvent>,
		ISagaAction<OrderPaymentApprovedEvent>,
		ISagaAction<OrderPaymentRejectedEvent>
    {
		private readonly IRepository<Order, Guid> _repository;

        public CreateOrderSaga(IRepository<Order, Guid> repository)
        {
			_repository = repository;
        }

        public async Task HandleAsync(OrderApprovedEvent message, ISagaContext context)
        {
            Data.OrderId = message.OrderId;
			// await _busPublisher.PublishAsync("topic", new StockReserveOrderEvent(message.OrderId));
        }

        public Task CompensateAsync(OrderApprovedEvent message, ISagaContext context)
        {
            return RejectAsync();
        }

        public Task HandleAsync(OrderStockReservedEvent message, ISagaContext context)
        {
            // await _busPublisher.PublishAsync("topic", new PayOrderEvent(message.OrderId));
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

        public Task HandleAsync(OrderPaymentApprovedEvent message, ISagaContext context)
        {
            throw new NotImplementedException();
        }

        public Task CompensateAsync(OrderPaymentApprovedEvent message, ISagaContext context)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(OrderPaymentRejectedEvent message, ISagaContext context)
        {
            throw new NotImplementedException();
        }

        public Task CompensateAsync(OrderPaymentRejectedEvent message, ISagaContext context)
        {
            throw new NotImplementedException();
        }
    }
}
