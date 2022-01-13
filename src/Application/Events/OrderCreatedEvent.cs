using System;
using System.Collections.Generic;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Application.Events
{
	public class OrderApprovedEvent : IEvent
	{
		public Guid OrderId { get; set; }
		public Guid CustomerId { get; set; }
		public IList<Product> Products { get; set; }
	}

	public class Product
	{
		public Guid Id { get; set; }
		public int Quantity { get; set; }
	}
}
