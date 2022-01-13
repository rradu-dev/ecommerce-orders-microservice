using System.Threading.Tasks;

namespace Ecommerce.Services.Orders.Application.MessageBroker
{
	public interface IEventHandler<TEvent> where TEvent : IEvent
	{
		Task HandleAsync(TEvent @event);
	}
}
