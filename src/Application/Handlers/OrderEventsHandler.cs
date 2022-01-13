using System.Threading.Tasks;
using Chronicle;
using Ecommerce.Services.Orders.Application.Events;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Application.Handlers
{
    public class OrderEventsHandler :
        IEventHandler<OrderStockReservedEvent>,
        IEventHandler<OrderStockRejectedEvent>,
        IEventHandler<OrderPaymentApprovedEvent>,
        IEventHandler<OrderPaymentRejectedEvent>
    {
		private readonly ISagaCoordinator _coordinator;

		public OrderEventsHandler(ISagaCoordinator coordinator)
		{
			_coordinator = coordinator;
		}

        public Task HandleAsync(OrderStockReservedEvent @event)
			=> HandleSagaAsync(@event);

        public Task HandleAsync(OrderStockRejectedEvent @event)
			=> HandleSagaAsync(@event);

        public Task HandleAsync(OrderPaymentApprovedEvent @event)
			=> HandleSagaAsync(@event);

        public Task HandleAsync(OrderPaymentRejectedEvent @event)
			=> HandleSagaAsync(@event);

		private Task HandleSagaAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
            => _coordinator.ProcessAsync(@event, SagaContext.Empty);
    }
}
