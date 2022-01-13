using System;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Application.Events
{
	public class OrderPaymentRejectedEvent : IEvent
	{
		public Guid OrderId { get; set; }
	}
}
