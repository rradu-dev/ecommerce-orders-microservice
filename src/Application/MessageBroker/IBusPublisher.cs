using System.Threading.Tasks;

namespace Ecommerce.Services.Orders.Application.MessageBroker
{
	public interface IBusPublisher<TEvent> where TEvent : class, IEvent
	{
		Task PublishAsync(string topicName, TEvent @event);
	}
}
