using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Infrastructure.MessageBroker.Kafka
{
    internal class KafkaBusPublisher<TEvent> : IBusPublisher<TEvent>,
		IDisposable where TEvent : class, IEvent
    {
		private readonly IProducer<Guid, TEvent> _producer;

        public async Task PublishAsync(string topicName, TEvent @event)
        {
			var message = new Message<Guid, TEvent>
			{
				Key = Guid.NewGuid(),
				Value = @event
			};

			await _producer.ProduceAsync(topicName, message);
        }

        public void Dispose()
        {
			_producer.Dispose();
        }
    }
}
