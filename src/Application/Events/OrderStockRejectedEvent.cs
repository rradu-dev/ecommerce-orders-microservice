using System;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Application.Events
{
	public class OrderStockRejectedEvent : IEvent
	{
		public Guid CustomerId { get; set; }
		public Guid OrderId { get; set; }
	}
}
