using System;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Application.Events
{
	public class OrderStockReservedEvent : IEvent
	{
		public Guid OrderId { get; set; }
	}
}
