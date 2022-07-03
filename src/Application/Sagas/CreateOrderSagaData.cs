using System;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Application.Sagas
{
	public class CreateOrderSagaData
	{
		public Guid CustomerId { get; set; }
		public Guid OrderId { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}
