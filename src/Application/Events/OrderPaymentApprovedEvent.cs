using System;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Application.Events
{
	public class OrderPaymentApprovedEvent : IEvent
	{
		public Guid OrderId { get; set; }
	}
}
